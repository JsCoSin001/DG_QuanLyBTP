using System;
using System.IO.Ports;
using System.Text;
using System.Globalization;

public sealed class EscPosPrinter : IDisposable
{
    private readonly SerialPort _port;
    /// <summary>
    /// Tự động bỏ dấu tiếng Việt khi gửi text nếu máy in không hỗ trợ codepage.
    /// </summary>
    public bool StripVietnameseDiacritics { get; set; } = true;

    /// <summary>
    /// Bảng mã để encode text. Thử 1258 (Vietnamese) trước; nếu máy không hỗ trợ hãy đổi sang 850/437.
    /// </summary>
    public Encoding TextEncoding { get; set; }

    public EscPosPrinter(string comPort, int baudRate)
    {
        // Chọn encoding mặc định “an toàn”
        try { TextEncoding = Encoding.GetEncoding(1258); } // Vietnamese (Windows)
        catch { TextEncoding = Encoding.GetEncoding(850); } // Latin-1/Western (fallback)

        _port = new SerialPort(comPort, baudRate, Parity.None, 8, StopBits.One)
        {
            Handshake = Handshake.None,
            Encoding = Encoding.Default, // KHÔNG dùng _port.Encoding để gửi bytes ESC/POS
            DtrEnable = true,             // một số máy cần DTR/RTS
            RtsEnable = true,
            ReadTimeout = 2000,
            WriteTimeout = 2000,
            NewLine = "\n"
        };

        if (!_port.IsOpen)
            _port.Open();
    }

    // =========================
    // Các lệnh cơ bản ESC/POS
    // =========================

    public void Initialize()
    {
        // ESC @
        SendBytes(new byte[] { 0x1B, 0x40 });
    }

    public void AlignLeft() => SetAlign(0);
    public void AlignCenter() => SetAlign(1);
    public void AlignRight() => SetAlign(2);

    private void SetAlign(byte n)
    {
        // ESC a n  (0=left,1=center,2=right)
        SendBytes(new byte[] { 0x1B, 0x61, n });
    }

    /// <summary>
    /// Gửi nội dung text như Build trong CreateContentLabel.
    /// </summary>
    public void Write(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        if (StripVietnameseDiacritics)
            text = RemoveDiacritics(text);

        // Gợi ý: nếu cần cưỡng ép CRLF cho mỗi dòng, thay \n bằng \r\n.
        var bytes = TextEncoding.GetBytes(text);
        SendBytes(bytes);
    }

    /// <summary>
    /// Đẩy giấy N dòng.
    /// </summary>
    public void Feed(int lines)
    {
        if (lines < 0) lines = 0;
        // ESC d n
        SendBytes(new byte[] { 0x1B, 0x64, (byte)Math.Min(lines, 255) });
    }

    /// <summary>
    /// Cắt giấy (full cut mặc định).
    /// </summary>
    public void Cut(bool fullCut = true)
    {
        // GS V m
        // m = 0|48: full, m = 1|49: partial (tùy máy)
        byte m = (byte)(fullCut ? 0x00 : 0x01);
        SendBytes(new byte[] { 0x1D, 0x56, m });
    }

    // =========================
    // In QR theo chuẩn Epson ESC/POS (GS ( k)
    // =========================

    /// <summary>
    /// In QR Code với kích thước module (1..16). Mặc định 6.
    /// </summary>
    public void PrintQR(string data, int size = 6, char errorLevel = 'M')
    {
        if (data == null) data = string.Empty;

        // Ràng buộc size (module size)
        if (size < 1) size = 1;
        if (size > 16) size = 16;

        // 1) Chọn model QR: Model 2
        // GS ( k  pL pH  49 65 m 0
        // pL pH = 4,0
        SendBytes(new byte[] { 0x1D, 0x28, 0x6B, 0x04, 0x00, 0x31, 0x41, 0x32, 0x00 });

        // 2) Kích thước module (pixel mỗi chấm)
        // GS ( k  pL pH  49 67 n
        // pL pH = 3,0 ; n = 1..16
        SendBytes(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43, (byte)size });

        // 3) Mức sửa lỗi: L(48), M(49), Q(50), H(51)
        byte ec = (byte)(errorLevel == 'L' ? 48 :
                         errorLevel == 'M' ? 49 :
                         errorLevel == 'Q' ? 50 : 51);
        SendBytes(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x45, ec });

        // 4) Store data
        // data bytes theo encoding 8-bit (UTF-8 không phải lúc nào cũng OK; nhiều máy yêu cầu 8-bit single-byte)
        var payload = TextEncoding.GetBytes(data);
        int len = payload.Length + 3;
        byte pL = (byte)(len & 0xFF);
        byte pH = (byte)((len >> 8) & 0xFF);

        // GS ( k  pL pH  49 80 48  ...data...
        byte[] header = new byte[] { 0x1D, 0x28, 0x6B, pL, pH, 0x31, 0x50, 0x30 };
        SendBytes(Concat(header, payload));

        // 5) In QR
        // GS ( k  3 0  49 81 48
        SendBytes(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x51, 0x30 });
    }

    // =========================
    // Hạ tầng gửi dữ liệu
    // =========================

    private void SendBytes(byte[] data)
    {
        if (data == null || data.Length == 0) return;
        if (_port == null || !_port.IsOpen)
            throw new InvalidOperationException("Cổng COM chưa mở.");

        _port.Write(data, 0, data.Length);
    }

    private static byte[] Concat(byte[] a, byte[] b)
    {
        var r = new byte[a.Length + b.Length];
        Buffer.BlockCopy(a, 0, r, 0, a.Length);
        Buffer.BlockCopy(b, 0, r, a.Length, b.Length);
        return r;
    }

    private static string RemoveDiacritics(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        string normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalized.Length);

        foreach (var ch in normalized)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                // thay thế đ/Đ thủ công
                if (ch == 'đ') sb.Append('d');
                else if (ch == 'Đ') sb.Append('D');
                else sb.Append(ch);
            }
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    // =========================
    // IDisposable
    // =========================
    public void Dispose()
    {
        try
        {
            if (_port != null)
            {
                if (_port.IsOpen)
                {
                    try { _port.BaseStream.Flush(); } catch { }
                    _port.Close();
                }
                _port.Dispose();
            }
        }
        catch { /* nuốt lỗi đóng cổng */ }
    }
}

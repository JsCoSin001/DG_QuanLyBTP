using System;
using System.Runtime.InteropServices;
using System.Text;

namespace QLDuLieuTonKho_BTP.Data
{
    /// <summary>
    /// In QR code native lên SATO WS412TT-STD bằng SBPL (ESC+BQ) qua RAW Winspool.
    /// Tương thích C# 7.3 (không dùng switch expression hay pattern matching mới).
    /// </summary>
    internal static class Sato_WS412TT_STD_QrPrinter
    {
        private const string ESC = "\x1B";

        /// <summary>
        /// Mức sửa lỗi QR.
        /// </summary>
        public enum Ecc { L, M, Q, H }

        /// <summary>
        /// In QR code native trên máy in SATO WS412TT-STD bằng SBPL.
        /// </summary>
        /// <param name="printerName">
        /// Tên máy in trong Windows.  
        /// 👉 Ví dụ: "SATO WS412TT", "SATO WS412TT-USB", hoặc tên hiển thị trong Control Panel/Printers.
        /// </param>
        /// <param name="content">
        /// Nội dung QR (ASCII/UTF-8, không nhị phân).  
        /// 👉 Ví dụ: "https://example.com/order/12345", "ABC123456".
        /// </param>
        /// <param name="x">
        /// Vị trí ngang (đơn vị dot, 300 dpi ≈ 11.8 dot/mm).  
        /// 👉 Gợi ý: 0–800 (tùy chiều rộng tem).
        /// </param>
        /// <param name="y">
        /// Vị trí dọc (dot).  
        /// 👉 Gợi ý: 0–600 (tùy chiều cao tem).
        /// </param>
        /// <param name="cellSize">
        /// Kích thước 1 ô QR (dot).  
        /// 👉 Giá trị hợp lệ: 1–32.  
        /// 👉 Gợi ý: 6–10 (tương đương ~0.5–0.85 mm/module) để dễ quét.
        /// </param>
        /// <param name="eccLevel">
        /// Mức sửa lỗi QR.  
        /// 👉 Gợi ý: <see cref="Ecc.L"/> (7%), <see cref="Ecc.M"/> (15%),  
        /// <see cref="Ecc.Q"/> (25%), <see cref="Ecc.H"/> (30%).
        /// </param>
        /// <param name="quantity">
        /// Số nhãn in.  
        /// 👉 Gợi ý: 1–999.
        /// </param>
        /// <param name="concatMode">
        /// Chế độ ghép nhiều phần (structured append).  
        /// 👉 0 = bình thường (thường dùng), 1 = concat nhiều phần (ít khi dùng).
        /// </param>
        public static void PrintQr(
            string printerName,
            string content,
            int x, int y,
            int cellSize = 10,
            Ecc eccLevel = Ecc.M,
            int quantity = 1,
            int concatMode = 0)
        {
            if (string.IsNullOrEmpty(printerName)) throw new ArgumentNullException(nameof(printerName));
            if (string.IsNullOrEmpty(content)) throw new ArgumentNullException(nameof(content));
            if (cellSize < 1 || cellSize > 32) throw new ArgumentOutOfRangeException(nameof(cellSize));
            if (quantity < 1) quantity = 1;
            if (concatMode != 0 && concatMode != 1) throw new ArgumentOutOfRangeException(nameof(concatMode));

            string sbpl = BuildQrSbpl(content, x, y, cellSize, eccLevel, quantity, concatMode);
            RawPrinterHelper.SendStringToPrinter(printerName, sbpl);
        }

        // Tạo chuỗi SBPL theo manual WS4 (dạng auto-setup cho ESC+BQ)
        private static string BuildQrSbpl(string data, int x, int y, int cell, Ecc ecc, int qty, int concatMode)
        {
            // Mapping ECC -> tham số 'a' của BQ
            string a;
            switch (ecc)
            {
                case Ecc.L: a = "1"; break;
                case Ecc.M: a = "2"; break;
                case Ecc.Q: a = "4"; break;
                case Ecc.H: a = "3"; break;
                default: a = "2"; break; // mặc định M
            }

            string b = (concatMode == 1) ? "1" : "0";
            string cc = cell.ToString("00"); // 2 chữ số
            string xStr = x.ToString("0000");
            string yStr = y.ToString("0000");

            var sb = new StringBuilder(256 + data.Length);
            sb.Append(ESC).Append('A');            // Start format
            sb.Append(ESC).Append('V').Append(yStr);
            sb.Append(ESC).Append('H').Append(xStr);

            if (concatMode == 0)
            {
                // Auto-setup, không gửi (ddeeff,)
                sb.Append(ESC).Append("BQ").Append(a).Append(b).Append(cc).Append(',').Append(data);
            }
            else
            {
                // Mặc định 1 phần (d=01), phần 1 (e=01), parity=00 (demo)
                sb.Append(ESC).Append("BQ").Append(a).Append(b).Append(cc).Append(",010100,").Append(data);
            }

            sb.Append("\r\n");
            sb.Append(ESC).Append('Q').Append(qty); // số nhãn
            sb.Append("\r\n");
            sb.Append(ESC).Append('Z');             // End format
            sb.Append("\r\n");

            return sb.ToString();
        }

        /// <summary>
        /// Helper gửi dữ liệu RAW tới máy in qua Winspool (tương thích .NET Framework/.NET).
        /// </summary>
        private static class RawPrinterHelper
        {
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            private class DOC_INFO_1
            {
                public string pDocName;
                public string pOutputFile;
                public string pDataType;
            }

            [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Unicode)]
            private static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", SetLastError = true)]
            private static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true, CharSet = CharSet.Unicode)]
            private static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In] DOC_INFO_1 di);

            [DllImport("winspool.Drv", SetLastError = true)]
            private static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true)]
            private static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true)]
            private static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true)]
            private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

            public static void SendStringToPrinter(string printerName, string sbpl)
            {
                IntPtr hPrinter;
                IntPtr pBytes = IntPtr.Zero;

                if (!OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                    throw new InvalidOperationException("Không mở được máy in: " + printerName);

                try
                {
                    var di = new DOC_INFO_1
                    {
                        pDocName = "SBPL QR",
                        pDataType = "RAW"
                    };

                    if (!StartDocPrinter(hPrinter, 1, di))
                        throw new InvalidOperationException("StartDocPrinter thất bại.");

                    if (!StartPagePrinter(hPrinter))
                        throw new InvalidOperationException("StartPagePrinter thất bại.");

                    byte[] bytes = Encoding.ASCII.GetBytes(sbpl);
                    pBytes = Marshal.AllocHGlobal(bytes.Length);
                    Marshal.Copy(bytes, 0, pBytes, bytes.Length);

                    int written;
                    if (!WritePrinter(hPrinter, pBytes, bytes.Length, out written) || written != bytes.Length)
                        throw new InvalidOperationException("WritePrinter thất bại.");

                    EndPagePrinter(hPrinter);
                    EndDocPrinter(hPrinter);
                }
                finally
                {
                    if (pBytes != IntPtr.Zero) Marshal.FreeHGlobal(pBytes);
                    ClosePrinter(hPrinter);
                }
            }
        }
    }
}

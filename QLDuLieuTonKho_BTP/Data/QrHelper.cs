using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using QRCoder;

public static class QrHelper
{
    public static Bitmap CreateQrBitmap(
        string content,
        int moduleSize = 6,
        int marginModules = 2,
        bool to1bpp = true,
        QRCodeGenerator.ECCLevel ecc = QRCodeGenerator.ECCLevel.M)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("content is null or empty", nameof(content));
        if (moduleSize < 2) moduleSize = 2;
        if (marginModules < 0) marginModules = 0;

        Bitmap withMargin;
        using (var generator = new QRCodeGenerator())
        {
            var data = generator.CreateQrCode(
                plainText: content,
                eccLevel: ecc,
                forceUtf8: true,
                utf8BOM: false,
                eciMode: QRCodeGenerator.EciMode.Utf8);

            using (var qr = new QRCode(data))
            using (var raw = qr.GetGraphic(moduleSize, Color.Black, Color.White, drawQuietZones: false))
            {
                int margin = marginModules * moduleSize;
                withMargin = new Bitmap(raw.Width + margin * 2, raw.Height + margin * 2, PixelFormat.Format24bppRgb);
                using (var g = Graphics.FromImage(withMargin))
                {
                    g.Clear(Color.White);
                    g.DrawImageUnscaled(raw, margin, margin);
                }
            }
        }

        if (to1bpp)
        {
            var one = To1BppManaged(withMargin, 200);
            withMargin.Dispose();
            return one;
        }
        return withMargin;
    }

    /// <summary>
    /// Chuyển Bitmap (24/32bpp) về 1-bpp **không dùng unsafe**.
    /// </summary>
    public static Bitmap To1BppManaged(Bitmap source, byte threshold = 200)
    {
        // Đảm bảo nguồn là 24bpp để tính chỉ số RGB đơn giản
        Bitmap src24 = source.PixelFormat == PixelFormat.Format24bppRgb
            ? (Bitmap)source.Clone()
            : source.Clone(new Rectangle(0, 0, source.Width, source.Height), PixelFormat.Format24bppRgb);

        int w = src24.Width;
        int h = src24.Height;

        var dest = new Bitmap(w, h, PixelFormat.Format1bppIndexed);

        BitmapData srcData = null;
        BitmapData dstData = null;

        try
        {
            srcData = src24.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            dstData = dest.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            int srcStride = srcData.Stride;
            int dstStride = dstData.Stride;

            var srcRowBuf = new byte[srcStride];
            var dstRowBuf = new byte[dstStride];

            for (int y = 0; y < h; y++)
            {
                // đọc 1 dòng nguồn
                Marshal.Copy(srcData.Scan0 + y * srcStride, srcRowBuf, 0, srcStride);
                Array.Clear(dstRowBuf, 0, dstRowBuf.Length);

                for (int x = 0; x < w; x++)
                {
                    int idx = x * 3;      // 24bpp: B,G,R
                    byte b = srcRowBuf[idx + 0];
                    byte g = srcRowBuf[idx + 1];
                    byte r = srcRowBuf[idx + 2];

                    int gray = (r * 299 + g * 587 + b * 114) / 1000;
                    int bit = (gray < threshold) ? 1 : 0;

                    int byteIndex = x >> 3;       // x/8
                    int bitIndex = 7 - (x & 7);  // bit cao trước
                    dstRowBuf[byteIndex] |= (byte)(bit << bitIndex);
                }

                // ghi 1 dòng đích
                Marshal.Copy(dstRowBuf, 0, dstData.Scan0 + y * dstStride, dstStride);
            }
        }
        finally
        {
            if (srcData != null) src24.UnlockBits(srcData);
            if (dstData != null) dest.UnlockBits(dstData);
            src24.Dispose();
        }

        // Palette 1-bpp: 0=trắng, 1=đen
        var pal = dest.Palette;
        pal.Entries[0] = Color.White;
        pal.Entries[1] = Color.Black;
        dest.Palette = pal;

        return dest;
    }
}

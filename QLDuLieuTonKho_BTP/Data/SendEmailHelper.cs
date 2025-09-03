using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public sealed class SmtpConfig
{
    public string Host = "smtp.gmail.com";
    public int Port = 587;                
    public bool EnableSsl = true;         

    public string UserName = "";
    public string Password = "";          
    public string FromEmail = "";
    public string FromDisplayName = "";
}


public static class SendEmailHelper
{
    static SmtpConfig smtp = SmtpSecrets.Config;

    // [CHANGE] THÊM THAM SỐ useBcc ĐỂ CHỌN GỬI 1-EMAIL/BCC HAY GỬI LẦN LƯỢT
    public static async Task SendEmail(List<string> recipients, string body, bool useBcc = true)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
        catch { }

        string subject = "[CẢNH BÁO] KHỐI LƯỢNG PHẾ VƯỢT TIÊU CHUẨN (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";

        try
        {
            Debug.WriteLine($"[EMAIL] Bắt đầu gửi {recipients?.Count ?? 0} email(s) | Host={smtp?.Host}:{smtp?.Port} | SSL={smtp?.EnableSsl}");

            if (useBcc)
            {
                // [CHANGE] GỬI 1 EMAIL CHO NHIỀU NGƯỜI (BCC) – NHANH HƠN RẤT NHIỀU
                await SendOneEmailToManyAsync(recipients, subject, body, smtp);
            }
            else
            {
                // GIỮ LẠI CÁCH CŨ: GỬI LẦN LƯỢT
                await SendBulkEmailAsync(recipients, subject, body, smtp);
            }

            Debug.WriteLine("[EMAIL] Hoàn tất hàm SendEmail().");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[EMAIL][EX] Lỗi tổng: {ex.Message}");
            WriteEmailFailureLog(subject, body, recipients,
                new List<Tuple<string, string>> { Tuple.Create("SYSTEM", ComposeFullError(ex)) });
        }
    }

    // [CHANGE] HÀM MỚI: GỬI 1 EMAIL → NHIỀU NGƯỜI (BCC), CÓ CHIA LÔ AN TOÀN
    private static async Task SendOneEmailToManyAsync(
        IEnumerable<string> recipients,
        string subject,
        string htmlBody,
        SmtpConfig smtp,
        int maxBccPerEmail = 40,               // [CHANGE] số BCC tối đa mỗi email (điều chỉnh theo quota SMTP)
        CancellationToken ct = default)
    {
        var cleanList = recipients
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (cleanList.Count == 0)
        {
            Debug.WriteLine("[EMAIL][BCC] Không có người nhận hợp lệ.");
            return;
        }

        int emailIndex = 0;
        foreach (var batch in Chunk(cleanList, Math.Max(1, maxBccPerEmail)))
        {
            ct.ThrowIfCancellationRequested();
            emailIndex++;

            Debug.WriteLine($"[EMAIL][BCC] Chuẩn bị gửi lô #{emailIndex} với {batch.Count} người nhận...");

            using (var client = new SmtpClient())
            {
                client.Host = smtp.Host;
                client.Port = smtp.Port;
                client.EnableSsl = smtp.EnableSsl;

                client.UseDefaultCredentials = false; // [CHANGE] đảm bảo dùng credential tùy chỉnh
                client.Credentials = new NetworkCredential(smtp.UserName, smtp.Password);

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Timeout = 30000;

                using (var msg = new MailMessage())
                {
                    msg.From = new MailAddress(smtp.FromEmail, smtp.FromDisplayName);
                    msg.Subject = subject;
                    msg.Body = htmlBody;
                    msg.IsBodyHtml = true;

                    // [CHANGE] TẤT CẢ NGƯỜI NHẬN ĐƯỢC ADD VÀO BCC
                    foreach (var to in batch)
                        msg.Bcc.Add(to);

                    var sw = Stopwatch.StartNew();
                    try
                    {
                        // [CHANGE] ÁP DỤNG RETRY CHO TRƯỜNG HỢP LỖI TẠM THỜI
                        await WithRetryAsync<object>(async () =>
                        {
                            await client.SendMailAsync(msg);
                            return null;
                        });

                        sw.Stop();
                        Debug.WriteLine($"[EMAIL][BCC] ✓ THÀNH CÔNG lô #{emailIndex} | {sw.ElapsedMilliseconds} ms");
                    }
                    catch (Exception ex)
                    {
                        sw.Stop();
                        Debug.WriteLine($"[EMAIL][BCC] ✗ THẤT BẠI lô #{emailIndex} | {sw.ElapsedMilliseconds} ms | {ComposeFullError(ex)}");

                        // [CHANGE] GHI LOG CHI TIẾT LỖI + DANH SÁCH NGƯỜI NHẬN CỦA LÔ NÀY
                        WriteEmailFailureLog(subject, htmlBody, batch, new List<Tuple<string, string>>
                        {
                            Tuple.Create("BCC_BATCH", ComposeFullError(ex))
                        });
                    }
                }
            }
        }

        Debug.WriteLine($"[EMAIL][BCC] Hoàn tất gửi {cleanList.Count} người (chia {Math.Ceiling(cleanList.Count / (double)maxBccPerEmail)} lô).");
    }

    // Retry policy nhẹ cho lỗi tạm thời (421/4.x)
    private static async Task<TResult> WithRetryAsync<TResult>(Func<Task<TResult>> action, int maxAttempts = 5)
    {
        int attempt = 0;
        int delayMs = 1000; // 1s -> 2s -> 4s ...
        Exception lastEx = null;

        while (attempt < maxAttempts)
        {
            try
            {
                return await action();
            }
            catch (SmtpException ex) when (IsTransient(ex))
            {
                lastEx = ex;
                attempt++;
                Debug.WriteLine($"[EMAIL][RETRY] Thử lại #{attempt} sau {delayMs} ms | {ex.Message}");
                await Task.Delay(delayMs);
                delayMs *= 2;
            }
        }

        throw lastEx ?? new Exception("Retry exhausted without specific exception");
    }

    private static bool IsTransient(SmtpException ex)
    {
        var msg = (ex.Message ?? "").ToLowerInvariant();
        // 421 / 450 / 451 / 452 là phổ biến cho tạm thời
        return msg.Contains("421") || msg.Contains("450") || msg.Contains("451") || msg.Contains("452");
    }

    private static string ComposeFullError(Exception ex)
    {
        var sb = new StringBuilder();
        sb.Append(ex.GetType().Name)          // [FIX] Append (A hoa)
          .Append(": ")                       // [FIX] Append (A hoa)
          .Append(ex.Message);                // [FIX] Append (A hoa)

        if (ex is SmtpException smtpEx)
        {
            sb.Append(" | StatusCode=").Append(smtpEx.StatusCode);
        }
        if (ex.InnerException != null)
        {
            sb.Append(" | Inner: ")
              .Append(ex.InnerException.GetType().Name)
              .Append(": ")
              .Append(ex.InnerException.Message);
        }
        return sb.ToString();
    }

    // Gửi lần lượt → giữ lại để fallback khi không dùng BCC
    public static async Task SendBulkEmailAsync(
        IEnumerable<string> recipients,
        string subject,
        string htmlBody,
        SmtpConfig smtp,
        CancellationToken ct = default(CancellationToken))
    {
        var failed = new List<Tuple<string, string>>();
        var total = 0;
        var success = 0;

        Debug.WriteLine("[EMAIL] Khởi tạo SmtpClient...");
        using (var client = new SmtpClient())
        {
            client.Host = smtp.Host;
            client.Port = smtp.Port;
            client.EnableSsl = smtp.EnableSsl;

            // BẮT BUỘC khi dùng NetworkCredential tuỳ chỉnh
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtp.UserName, smtp.Password);

            // Một số môi trường cần set thêm để tránh treo lâu
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 30000; // 30s

            Debug.WriteLine("[EMAIL] SmtpClient sẵn sàng. Bắt đầu vòng lặp người nhận...");

            foreach (var to in recipients)
            {
                total++;
                ct.ThrowIfCancellationRequested();

                using (var msg = new MailMessage())
                {
                    // From phải trùng tài khoản đăng nhập Gmail để tránh 5.7.26
                    msg.From = new MailAddress(smtp.FromEmail, smtp.FromDisplayName);
                    msg.To.Add(to);
                    msg.Subject = subject;
                    msg.Body = htmlBody;
                    msg.IsBodyHtml = true;

                    var sw = Stopwatch.StartNew();
                    Debug.WriteLine($"[EMAIL][{total}] → Gửi đến: {to}");

                    try
                    {
                        await WithRetryAsync<object>(async () =>
                        {
                            await client.SendMailAsync(msg); // SMTP connect+auth+send xảy ra tại đây
                            return null;
                        });

                        sw.Stop();
                        success++;
                        Debug.WriteLine($"[EMAIL][{total}] ✓ THÀNH CÔNG đến {to} | {sw.ElapsedMilliseconds} ms");
                    }
                    catch (SmtpException smtpEx)
                    {
                        sw.Stop();
                        failed.Add(Tuple.Create(to, ComposeFullError(smtpEx)));
                        Debug.WriteLine($"[EMAIL][{total}] ✗ THẤT BẠI đến {to} | {sw.ElapsedMilliseconds} ms | {ComposeFullError(smtpEx)}");

                        // Gợi ý chẩn đoán nhanh Gmail bị chặn (thường 5.7.x/auth)
                        var m = smtpEx.Message;
                        if (m.Contains("5.7.0") || m.Contains("5.7.8") || m.Contains("5.7.9") || m.Contains("534") || smtpEx.StatusCode == SmtpStatusCode.GeneralFailure)
                        {
                            Debug.WriteLine("[EMAIL][HINT] Có thể bị chặn bởi Gmail (policy/auth). Kiểm tra: App Password 16 ký tự, đã bật 2FA, đúng cổng 587 + TLS, From trùng tài khoản.");
                        }
                    }
                    catch (Exception ex)
                    {
                        sw.Stop();
                        failed.Add(Tuple.Create(to, ComposeFullError(ex)));
                        Debug.WriteLine($"[EMAIL][{total}] ✗ THẤT BẠI đến {to} | {sw.ElapsedMilliseconds} ms | {ComposeFullError(ex)}");
                    }
                }
            }
        }

        Debug.WriteLine($"[EMAIL] KẾT THÚC: Tổng={total}, Thành công={success}, Thất bại={failed.Count}");

        if (failed.Count > 0)
        {
            WriteEmailFailureLog(subject, htmlBody, GetRecipientList(recipients), failed);

            foreach (var f in failed)
            {
                Debug.WriteLine($"[EMAIL][FAIL-DETAIL] {f.Item1} → {f.Item2}");
            }
        }
    }

    private static List<string> GetRecipientList(IEnumerable<string> recipients)
        => recipients is List<string> list ? list : new List<string>(recipients);

    // 🔹 Lấy thư mục Log ở cùng cấp với thư mục chứa exe
    private static string GetSiblingLogFolder()
    {
        var exeDir = AppDomain.CurrentDomain.BaseDirectory
            .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var parent = Directory.GetParent(exeDir) != null
            ? Directory.GetParent(exeDir).FullName
            : exeDir;
        var logDir = Path.Combine(parent, "Log");
        Directory.CreateDirectory(logDir); // đảm bảo tồn tại
        return logDir;
    }

    private static void WriteEmailFailureLog(
        string subject,
        string htmlBody,
        IEnumerable<string> recipients,
        List<Tuple<string, string>> failed)
    {
        var now = DateTime.Now;
        string title = now.ToString("yyyy-MM-dd HH:mm:ss");
        string fileName = "EmailError_" + now.ToString("yyyyMMdd_HHmmss") + ".txt";

        string folder = GetSiblingLogFolder();
        string path = Path.Combine(folder, fileName);

        var sb = new StringBuilder();
        sb.AppendLine(title);
        sb.AppendLine(new string('=', title.Length));
        sb.AppendLine("Có lỗi khi gửi email.");
        sb.AppendLine();

        sb.AppendLine("Subject:");
        sb.AppendLine(subject);
        sb.AppendLine();

        sb.AppendLine("Body (HTML):");
        sb.AppendLine(htmlBody);
        sb.AppendLine();

        sb.AppendLine("Recipients:");
        foreach (var r in recipients)
            sb.AppendLine(" - " + r);
        sb.AppendLine();

        sb.AppendLine("Chi tiết lỗi:");
        foreach (var f in failed)
            sb.AppendLine($" - {f.Item1}: {f.Item2}");

        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        Debug.WriteLine($"[EMAIL] Đã ghi log lỗi tại: {path}");
    }

    // [CHANGE] TIỆN ÍCH CHIA NHỎ LIST THEO KÍCH THƯỚC (DÙNG CHO BCC BATCH)
    private static IEnumerable<List<T>> Chunk<T>(IEnumerable<T> source, int size)
    {
        var batch = new List<T>(size);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count >= size)
            {
                yield return batch;
                batch = new List<T>(size);
            }
        }
        if (batch.Count > 0)
            yield return batch;
    }
}
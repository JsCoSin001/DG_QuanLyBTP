using QLDuLieuTonKho_BTP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace QLDuLieuTonKho_BTP
{
    public static class Helper
    {
        private static readonly Random _random = new Random();

        public static string LayKieuSP(string maSP)
        {
            string[] parts = maSP.Split('.');

            if (parts.Length != 2) return "";

            return parts[0].ToUpper();
        }

        public static string GetShiftValue()
        {
            int hour = DateTime.Now.Hour;

            if (hour >= 6 && hour < 14)
                return "1";

            if (hour >= 14 && hour < 22)
                return "2";

            return "3";
        }

        public static List<string> GetDSNguoiNhan()
        {
            string query = "SELECT Ten FROM DanhSachNhanTBLoi WHERE Active = 1";
            DataTable dt = DatabaseHelper.GetData(null, query, null);

            List<string> recipients = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                recipients.Add(row["Ten"].ToString());
            }

            return recipients;
        }

        public static string GetNgayHienTai()
        {
            DateTime now = DateTime.Now;
            DateTime ngayHienTai = (now.TimeOfDay < new TimeSpan(6, 0, 0))
                ? DateTime.Today.AddDays(-1)
                : DateTime.Today;

            return ngayHienTai.ToString("yyyy-MM-dd");
        }

        public static string GenerateRandomString(string congDoan, int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++) result.Append(chars[_random.Next(chars.Length)]);

            return "Z_" + DateTime.Now.ToString("yyyy-MM-dd hh_mm") + "-" + congDoan + "-" + result;
        }

        public static string GetURLDatabase()
        {
            string result = "";

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn file database (.db)";
                dialog.Filter = "SQLite Database (*.db)|*.db|Tất cả các file (*.*)|*.*";
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    result = dialog.FileName;
                }
                else
                {
                    result = "";
                }
            }

            Properties.Settings.Default.URL = result;
            Properties.Settings.Default.Save();

            return result;
        }

        public static void LoadUserControl(UserControl uc, Panel parent)
        {
            parent.Controls.Clear();      // Xóa UserControl cũ (nếu có)
            uc.Dock = DockStyle.Fill;        // Cho UserControl lấp đầy panel
            parent.Controls.Add(uc);      // Thêm vào panel
        }

        public static string GenerateSQL_GetData(string tableName, string dateInsert)
        {
            return $@"
            SELECT ID, Ma, Ten, DateInsert ,KieuSP
            FROM DanhSachMaSP 
            WHERE DateInsert >= '{dateInsert}' AND KieuSP = '{tableName}' 
            ORDER BY ID DESC";
        }

        public static string GenerateSQL_GetAll(string dateInsert)
        {
            string query = $@"
            SELECT ID, Ma, Ten,KieuSP ,DateInsert FROM DanhSachMaSP
            WHERE DateInsert >= '{dateInsert}'            
            ORDER BY KieuSP, ID DESC";
            return query;
        }

        public static string[] PhanTachLot(string input)
        {
            // Thay thế các ký tự phân cách bằng dấu chấm phẩy (;)
            string normalized = input.Replace("-", ";").Replace("/", ";");

            // Tách chuỗi theo dấu chấm phẩy
            string[] parts = normalized.Split(';', (char)StringSplitOptions.RemoveEmptyEntries);

            return parts;

        }

        public static string LOTGenerated(ComboBox may, NumericUpDown maHT, ComboBox sttCongDoan, NumericUpDown sttBin, NumericUpDown soBin)
        {
            string lot = "";

            // Kiểm tra maHT có đủ 6 chữ số
            int maHTValue = (int)maHT.Value;
            if (maHTValue < 100000 || maHTValue > 999999)
                return lot;

            // Kiểm tra ComboBox 'may'
            if (may.SelectedItem == null ||
                string.IsNullOrWhiteSpace(may.Text) ||
                may.Text == "0")
                return lot;

            // Kiểm tra sttCongDoan
            if (sttCongDoan.SelectedItem == null ||
                string.IsNullOrWhiteSpace(sttCongDoan.Text) ||
                sttCongDoan.Text == "0")
                return lot;

            // Kiểm tra sttBin và soBin
            if (sttBin.Value == 0)
                return lot;

            string sttBinT = sttBin.Value < 10 ? "0" + sttBin.Text : sttBin.Text;
            string soBinT = soBin.Value < 10 ? "0" + soBin.Text : soBin.Text;

            // Tạo mã LOT: may-maHT-sttCongDoan-sttBin-soBin
            lot = $"{may.Text}-{maHTValue}/{sttCongDoan.Text}-{sttBinT}-{soBinT}";

            return lot;
        }

        public static Uc_ShowData LoadUserControlsWithData<T>(Panel pnLeft, Panel pnRight, out T leftControl, Action<DataTable> onDataReadyCallback, params object[] constructorArgs) where T : UserControl, ICustomUserControl
        {
            leftControl = (T)Activator.CreateInstance(typeof(T), constructorArgs);
            var rightControl = new Uc_ShowData();

            pnLeft.Width = leftControl.Width;
            pnLeft.Height = leftControl.Height;

            leftControl.OnDataReady += onDataReadyCallback;

            LoadUserControl(leftControl, pnLeft);
            LoadUserControl(rightControl, pnRight);

            return rightControl;
        }

        public static void AddHoverEffect(Label lbl)
        {
            // Lưu font gốc để khôi phục đúng family/size
            var originalFont = lbl.Font;
            var hoverFont = new System.Drawing.Font(originalFont, FontStyle.Italic | FontStyle.Underline);

            lbl.MouseEnter += (s, e) =>
            {
                lbl.Font = hoverFont;
                lbl.ForeColor = System.Drawing.Color.Blue;
                lbl.Cursor = Cursors.Hand;
            };

            lbl.MouseLeave += (s, e) =>
            {
                lbl.Font = originalFont;          // trở về đứng (Regular)
                lbl.ForeColor = System.Drawing.Color.Black;      // màu đen
                lbl.Cursor = Cursors.Default;
            };

            // Dọn dẹp font tạo ra khi label bị dispose
            lbl.Disposed += (s, e) => hoverFont.Dispose();
        }

        public interface ICustomUserControl { event Action<DataTable> OnDataReady; }

        public static void UpdatePassApp(string tb)
        {
            string[] parts = tb.Split('|');

            if (parts.Count() == 2 && parts[0] == "Change")
                Properties.Settings.Default.PassApp = parts[1].Trim();
            else
                Properties.Settings.Default.UserPass = tb;

            Properties.Settings.Default.Save();
            // Khởi động lại ứng dụng
            Application.Restart();

            // Thoát ứng dụng hiện tại
            Environment.Exit(0);
        }

        public static bool kiemTraPhanQuyen(string tx)
        {
            string password = Properties.Settings.Default.PassApp;
            if (tx == password)
            {
                return true;
            }
            else
            {
                MessageBox.Show("BẠN CẦN CẤP QUYỀN ĐỂ SỬ DỤNG CHỨC NĂNG NÀY!.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /* Tỷ lệ phế liệu đồng cả tổ bọc mục tiêu <= 0.7 % : type= "dong",
        Tỷ lệ phế liệu cả tổ bọc nhựa mục tiêu  <= 1.5 % : type = "nhua".
        Tính chung cả đồng và nhựa tạm tính 1.1 % : type = "tong"
        */
        public static bool needToSendEmail(decimal tyLePhe, string type = "dong")
        {
            bool result = false;

            switch (type)
            {
                case "dong":
                    if (tyLePhe > 0.007m)
                        result = true;
                    break;
                case "nhua":
                    if (tyLePhe > 0.025m)
                        result = true;
                    break;
                default:
                    if (tyLePhe > 0.011m)
                        result = true;
                    break;
            }

            return result;
        }

        public static string TaoKhoangTrong(int tongKhoangTrong, string noiDung)
        {
            return new string(' ', tongKhoangTrong - noiDung.Length);

        }




    }
}


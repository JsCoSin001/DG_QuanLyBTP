using DocumentFormat.OpenXml.Wordprocessing;
using PdfiumViewer;
using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

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

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }

            return "Z_" + DateTime.Now.ToString("yyyy-MM-dd") + "-" + congDoan + "-" + result;
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

        public static DataTable GetKLTruocSX(ComboBox may, NumericUpDown maHT, ComboBox sttCongDoan, NumericUpDown sttBin, NumericUpDown soBin, TextBox lotNumber)
        {
            DataTable resultTable = new DataTable();
            string lot = "";
            lot = LOTGenerated( may,  maHT,  sttCongDoan,  sttBin,  soBin);

            if (lot == "") return resultTable;

            lotNumber.Text = lot;

            string para = "Lot";
            string query = @"
                SELECT 
                    DL_CD_Ben.ID  as id,
                    TonKho.KhoiLuongConLai as KhoiLuongConLai
                FROM 
                    DL_CD_Ben
                JOIN 
                    TonKho ON DL_CD_Ben.TonKho_ID = TonKho.ID
                WHERE 
                    TonKho.Lot = @" + para + ";";

            resultTable = DatabaseHelper.GetData(lot, query,para);

            return resultTable;
        }

        public static void FillKhoiLuongVaIDBen(DataTable dt, NumericUpDown id, NumericUpDown kl)
        {
            
            if (dt.Rows.Count > 0)
            {
                id.Value = Convert.ToInt32(dt.Rows[0]["ID"]);
                if (dt.Rows[0]["KhoiLuongConLai"] != DBNull.Value)
                    kl.Value = Convert.ToDecimal(dt.Rows[0]["KhoiLuongConLai"]);
                else
                    kl.Value = 0;
            }
            else
            {
                id.Value = 0; kl.Value = 0;
            }

        }

        // Tạo mã LOT theo định dạng: may-maHT/sttCongDoan-sttBin-soBin
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

            // Tạo mã LOT: may-maHT-sttCongDoan-sttBin-soBin
            lot = $"{may.Text}-{maHTValue}/{sttCongDoan.Text}-{sttBinT}-{soBin.Value}";

            return lot;
        }
                
        public static void RunEvent(ComboBox may, NumericUpDown maHT, ComboBox sttCongDoan, NumericUpDown sttBin, NumericUpDown soBin, TextBox lotNumber, NumericUpDown idBen, NumericUpDown klTruocBoc)
        {
            DataTable resultTable = new DataTable();
            resultTable = Helper.GetKLTruocSX(may, maHT, sttCongDoan, sttBin, soBin, lotNumber);

            Helper.FillKhoiLuongVaIDBen(resultTable, idBen, klTruocBoc);
        }
                
        public static Uc_ShowData LoadUserControlsWithData<T>(Panel pnLeft,  Panel pnRight, out T leftControl,  Action<DataTable> onDataReadyCallback, params object[] constructorArgs) where T : UserControl, ICustomUserControl
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
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }



        #region // In Tem với kích thước 80mm        
        public static string CreateContentLabel(TemSP temSP)
        {
            var space = new Dictionary<string, string>
            {
                ["dong1"] = new string(' ',10),
                ["dong2"] = new string(' ',10),
                ["dong3"] = new string(' ',10),
                ["dong4"] = new string(' ',10),
            };

            int tongKiTu = 48;

            string content = "PHIẾU QUẢN LÝ SẢN PHẨM" + space["dong1"] + "BQ-ISO-09-08" + "\n\n";
            content += new string('-', tongKiTu) + "\n";
            content += "Ngày SX: " + temSP.NgaySX + space["dong2"] + "Ca: " + temSP.ca + "\n";
            content +=  "Số lượng: " + temSP.SoLuong + "(m) "+ space["dong3"] + " - " + space["dong3"]+ temSP.KhoiLuong + "(Kg)" + "\n";
            content += "Màu SP: " + temSP.MauSP + "\n";
            content += "Mã SP: " + temSP.SoHanhTrinh + "\n";
            content += "Quy cách: " + temSP.QuyCach + "\n";
            content += "Đánh giá Chất lượng: " + temSP.DanhGiaChatLuong + "\n";
            content += "CN vận hành: " + temSP.TenCongNhan + "\n";
            content += "Nội dung lưu ý" +"\n";
            content += temSP.GhiChu + "\n";
            content += "QR Code: " + space["dong4"] + "KCS\n";

            return content;
        }

        public static void PrintLabelWithQr(string comPort, int baud, TemSP temSP, string qrData)
        {
            using (var p = new EscPosPrinter(comPort, baud))
            {
                p.Initialize();
                p.AlignLeft();

                // 1) In phần text như bạn xây dựng
                string content = CreateContentLabel(temSP);
                // Khuyến nghị: bỏ dấu tiếng Việt nếu máy không hỗ trợ
                p.Write(content);

                // 2) Chèn vài dòng trống cho đẹp
                p.Feed(1);

                // 3) In QR bên dưới
                p.AlignLeft(); // Qr code thường căn trái

                p.PrintQR(qrData, size: 6);

                p.Feed(3);
                p.Cut();
            }
        }

        #endregion

    }
}


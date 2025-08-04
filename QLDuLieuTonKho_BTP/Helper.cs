using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static void UpdateComboBox( List<ProductModel> items, ComboBox tbTenSP, TextBox tbMaSP, NumericUpDown idMaSP)
        {
            tbTenSP.DroppedDown = false;
            tbTenSP.SelectedIndexChanged -= (s, e) => OnComboBoxSelectionChanged(tbTenSP, tbMaSP, idMaSP);


            // check items count = 0
            if (items == null || items.Count == 0) return;
            string currentText = tbTenSP.Text;

            tbTenSP.DataSource = null; // Xóa nguồn dữ liệu cũ (nếu có)
            tbTenSP.DataSource = items;
            tbTenSP.DisplayMember = "Ten"; // Hiển thị tên trong ComboBox
            tbTenSP.ValueMember = "Ma";    // Giá trị bên trong ComboBox là mã (nếu cần dùng)

            tbTenSP.DroppedDown  = true;
            tbTenSP.Text = currentText;
            tbTenSP.SelectionStart = tbTenSP.Text.Length;
            tbTenSP.SelectionLength = 0;

            tbTenSP.SelectedIndexChanged += (s, e) => OnComboBoxSelectionChanged(tbTenSP, tbMaSP, idMaSP);

        }

        public static string GenerateRandomString(string congDoan, int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }

            return DateTime.Now + "-" + congDoan + result;
        }

        private static void OnComboBoxSelectionChanged(ComboBox comboBox, TextBox tbMaSP, NumericUpDown idMaSP)
        {
            if (comboBox.SelectedItem is ProductModel selectedProduct)
            {
                tbMaSP.Text = selectedProduct.Ma;
                idMaSP.Text = selectedProduct.ID.ToString();
            }
        }
                
        public static string GetURLDatabase()
        {
            string result = "";

            do
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Chọn file database (.db)";
                    dialog.Filter = "SQLite Database (*.db)|*.db|Tất cả các file (*.*)|*.*";
                    dialog.Multiselect = false;

                    if (dialog.ShowDialog() == DialogResult.OK) result = dialog.FileName;
                }
            } while (result == "");

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

        public static void LoadDlCdBenByDate(DataGridView showdata, string ngay, string connectionString)
        {
            if (showdata == null) throw new ArgumentNullException(nameof(showdata));
            if (string.IsNullOrWhiteSpace(ngay)) throw new ArgumentException("Giá trị ngày không hợp lệ.", nameof(ngay));
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Chuỗi kết nối không hợp lệ.", nameof(connectionString));

            try
            {
                //var db =  DatabaseHelper (connectionString);
                DataTable dt = DatabaseHelper.GetDataByDate(ngay, connectionString);

                void bind()
                {
                    showdata.AutoGenerateColumns = true;
                    showdata.DataSource = dt;

                    // Tuỳ chọn hiển thị
                    showdata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    showdata.ReadOnly = true;
                    showdata.AllowUserToAddRows = false;
                    showdata.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }

                if (showdata.InvokeRequired)
                    showdata.Invoke((Action)bind);
                else
                    bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DataTable GetKLTruocSX(ComboBox may, NumericUpDown maHT, ComboBox sttCongDoan, NumericUpDown sttBin, NumericUpDown soBin, TextBox lotNumber)
        {
            DataTable resultTable = new DataTable();
            string lot = "";
            lot = LOTGenerated( may,  maHT,  sttCongDoan,  sttBin,  soBin);

            if (lot == "") return resultTable;

            lotNumber.Text = lot;

            //const string query = "SELECT id, KhoiLuongConLai FROM TonKho WHERE Lot = @Lot COLLATE NOCASE";
            const string query = @"
                SELECT 
                    DL_CD_Ben.ID  as id,
                    TonKho.KhoiLuongConLai as KhoiLuongConLai
                FROM 
                    DL_CD_Ben
                JOIN 
                    TonKho ON DL_CD_Ben.TonKho_ID = TonKho.ID
                WHERE 
                    TonKho.Lot = @Lot;
                ";

            resultTable = DatabaseHelper.GetTonKho(lot, query);

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
            if (sttBin.Value == 0 || soBin.Value == 0)
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

        public static Uc_ShowData LoadUserControlsWithData<T>(
            Panel pnLeft,
            Panel pnRight,
            out T leftControl,
            Action<DataTable> onDataReadyCallback,
            params object[] constructorArgs
        ) where T : UserControl, ICustomUserControl
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


        public interface ICustomUserControl
        {
            event Action<DataTable> OnDataReady;
        }






    }
}

using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QLDuLieuTonKho_BTP.Helper;

namespace QLDuLieuTonKho_BTP
{
    public partial class Uc_BcTonKho : UserControl, ICustomUserControl
    {
        public event Action<DataTable> OnDataReady;

        private static readonly string _quyenMaster = Properties.Settings.Default.UserPass;
        public Uc_BcTonKho(string url)
        {
            InitializeComponent();
            DatabaseHelper.SetDatabasePath(url);
        }

        private async void btnTonKho_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT
                    TonKho.ID AS ID,
                    TonKho.Lot as Lot_TP,
                    DanhSachMaSP.Ma,
                    DanhSachMaSP.Ten,
                    TonKho.KhoiLuongDauVao AS DauVao,
                    TonKho.KhoiLuongConLai AS ConLai,
                    TonKho.ChieuDai,
                    DanhSachMaSP.KieuSP
                FROM
                    TonKho
                JOIN
                    DanhSachMaSP ON TonKho.MaSP_ID = DanhSachMaSP.ID
                WHERE
                    TonKho.KhoiLuongConLai > 0
                ORDER BY
                    TonKho.ID DESC;
                ";

            DataTable table = DatabaseHelper.GetDataFromSQL(sql);

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nào được tìm thấy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!cbXuatExcelReport.Checked)
            {
                OnDataReady?.Invoke(table);
                return;
            }


            cbXuatExcelReport.Checked = false;
            if (!Helper.kiemTraPhanQuyen(_quyenMaster)) return;
            cbXuatExcelReport.Checked = true;


            string fileName = "BC Ton Kho";
            await ExcelHelper.ExportWithLoading(table, fileName);

        }


        private void btnTimIDBen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maBin.Text)) return;

            const string sql = @"
                SELECT KhoiLuongConLai
                FROM TonKho
                WHERE Lot COLLATE NOCASE = @Lot
                AND KhoiLuongConLai <> 0
                LIMIT 1;";

            DataTable dt = DatabaseHelper.GetData(maBin.Text, sql, "Lot");

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Lot: " + maBin.Text + " đã hết hoặc không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                klBanTran.Value = 0;
                return;
            }

            klHienTai.Value = Convert.ToDecimal(dt.Rows[0]["KhoiLuongConLai"]);
        }
 

        private void klBanTran_KeyDown(object sender, KeyEventArgs e)
        {
           


        }

        private void klBanTran_ValueChanged(object sender, EventArgs e)
        {
            if (klHienTai.Value == 0)
            {
                MessageBox.Show("Cần tim mã bin trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                klBanTran.Value = 0;
                return;
            }

            klConLai.Value = klHienTai.Value - klBanTran.Value;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ConfigDB configDB = DatabaseHelper.GetConfig();

            if (!configDB.Active)
            {
                MessageBox.Show(configDB.Message, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (string.IsNullOrWhiteSpace(maBin.Text) || klBanTran.Value == 0)
            {
                MessageBox.Show("Dữ liệu không đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            bool fl = DatabaseHelper.UpdateKLBanTranAndKhoiLuongConLaiByLot(maBin.Text, klBanTran.Value, klConLai.Value);

            if (fl)
            {
                MessageBox.Show("Thao tác thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Thao tác thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}

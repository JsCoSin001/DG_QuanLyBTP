using QLDuLieuTonKho_BTP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            if(!cbXuatExcelReport.Checked)
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
    }
}

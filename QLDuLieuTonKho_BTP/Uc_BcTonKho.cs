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
        private string _url;
        public event Action<DataTable> OnDataReady;
        public Uc_BcTonKho(string url)
        {
            InitializeComponent();

            _url = url;
            DatabaseHelper.SetDatabasePath(url);
        }

        private async void btnTonKho_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT
                    TonKho.ID AS ID,
                    TonKho.Lot,
                    DanhSachMaSP.Ma,
                    DanhSachMaSP.Ten,
                    TonKho.KhoiLuongDauVao,
                    TonKho.KhoiLuongConLai,
                    TonKho.HanNoi,
                    TonKho.ChieuDai,
                    DanhSachMaSP.KieuSP
                FROM
                    TonKho
                JOIN
                    DanhSachMaSP ON TonKho.MaSP_ID = DanhSachMaSP.ID;
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

            string fileName = $"BC Ton Kho - {DateTime.Now:yyyy-MM-dd HH_mm}.xlsx";
            await ExcelHelper.ExportWithLoading(table, fileName);




            //using (SaveFileDialog sfd = new SaveFileDialog()
            //{
            //    Filter = "Excel Workbook|*.xlsx",
            //    FileName = "BC Ton Kho - "+ DateTime.Now.ToString("yyyy-MM-dd hh_mm") +".xlsx"
            //})
            //{
            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        string mes = "Đang xuất Excel, Xin hãy chờ ...";
            //        var loadingControl = new Uc_LoadingForm(mes);

            //        // 1. Tạo Form chứa UserControl (loading form)
            //        Form loadingForm = new Form
            //        {
            //            FormBorderStyle = FormBorderStyle.None,
            //            StartPosition = FormStartPosition.CenterScreen,
            //            Size = loadingControl.Size,
            //            ControlBox = false,
            //            TopMost = true,
            //            ShowInTaskbar = false
            //        };
            //        loadingControl.Dock = DockStyle.Fill;
            //        loadingForm.Controls.Add(loadingControl);

            //        // 2. Hiển thị loading form không chặn UI (trên UI thread)
            //        loadingForm.Show();

            //        // 3. Chạy export ở luồng nền
            //        await Task.Run(() =>
            //        {
            //            //var exporter = new ExcelHelper();
            //            ExcelHelper.ExportToExcel(table, sfd.FileName);
            //        });

            //        // 4. Đóng loading form an toàn trên UI thread
            //        if (loadingForm.InvokeRequired)
            //        {
            //            loadingForm.Invoke(new Action(() => loadingForm.Close()));
            //        }
            //        else
            //        {
            //            loadingForm.Close();
            //        }
            //    }

            //}
        }
    }
}

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

namespace QLDuLieuTonKho_BTP
{
    public partial class Main : Form
    {
        private string _url;
        private string _sign = "© 2025 - Made by Linh";

        private Uc_ShowData ucShowData;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            mySign.Text = _sign;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            _url = Properties.Settings.Default.URL;

            if (string.IsNullOrEmpty(_url))
            {
                MessageBox.Show("Vui lòng chọn đường dẫn đến file database.", "THÔNG BÁO");
                UrlDb.Text = Helper.GetURLDatabase();
                Application.Restart();
                return;
            }

            UrlDb.Text = _url;
        }

        private void btnTimDB_Click(object sender, EventArgs e)
        {
            string url = Helper.GetURLDatabase();
            MessageBox.Show("Ứng dụng sẽ được khởi động lại để áp dụng thay đổi.", "THÔNG BÁO");
            Application.Restart();
        }

        private void btnCapNhatMaSP_Click(object sender, EventArgs e)
        {
            ucShowData = Helper.LoadUserControlsWithData<Uc_CapNhatMaSP>(
                pnLeft,
                pnRight,
                out var ucCapNhatMaSP,
                dt => ucShowData.SetData(dt),
                _url
            );
        }

        private void btnBen_Click(object sender, EventArgs e)
        {
            string[] dsMay = new[]
            {
                "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10",
                "B11", "B12", "B13", "B14", "B15", "B16",
                "R6", "R10", "R12"
            };

            ucShowData = Helper.LoadUserControlsWithData<Uc_Ben>(
                pnLeft,
                pnRight,
                out var ucBen,
                dt => ucShowData.SetData(dt),
                _url
            );

            ucBen.LoadDanhSachMay(dsMay);
            ucBen.TypeOfProduct = "BTP";
            ucBen.TitleForm = "BÁO CÁO CÔNG ĐOẠN BỆN";
        }


        private void btnBocMach_Click(object sender, EventArgs e)
        {
            string[] dsMay = new[]
            {
                "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10",
                "E11", "E12", "E13", "E14", "E15"
            };

            int tenCongDoan = 1;

            ucShowData = Helper.LoadUserControlsWithData<Uc_Boc>(
                pnLeft,
                pnRight,
                out var ucBoc,
                dt => ucShowData.SetData(dt),
                _url, dsMay, tenCongDoan
            );
            ucBoc.LoadDanhSachMay(dsMay);
            ucBoc.TypeOfProduct = "BTP";
            
        }

        private void btnQuanMica_Click(object sender, EventArgs e)
        {
            string[] dsMay = new[]
            {
                "T3", "T4", "T5", "T6"
            };

            int tenCongDoan = 0;

            ucShowData = Helper.LoadUserControlsWithData<Uc_Boc>(
                pnLeft,
                pnRight,
                out var ucBoc,
                dt => ucShowData.SetData(dt),
                _url, dsMay, tenCongDoan
            );
            ucBoc.LoadDanhSachMay(dsMay);
            ucBoc.TypeOfProduct = "BTP";

        }

        private void btnBocVo_Click(object sender, EventArgs e)
        {
            
            string[] dsMay = new[]
            {
                "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10",
                "E11", "E12", "E13", "E14", "E15"
            };

            int tenCongDoan = 2;

            ucShowData = Helper.LoadUserControlsWithData<Uc_Boc>(
                pnLeft,
                pnRight,
                out var ucBoc,
                dt => ucShowData.SetData(dt),
                _url, dsMay, tenCongDoan
            );
            ucBoc.LoadDanhSachMay(dsMay);
            ucBoc.TypeOfProduct = "TP";
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            ucShowData = Helper.LoadUserControlsWithData<Uc_BcTonKho>(
                pnLeft,
                pnRight,
                out var ucBcTonKho,
                dt => ucShowData.SetData(dt),
                _url
            );
        }
    }
}

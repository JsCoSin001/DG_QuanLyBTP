using PdfiumViewer;
using QLDuLieuTonKho_BTP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDuLieuTonKho_BTP
{
    public partial class Main : Form
    {
        private string _url;
        private string _ver = "2.13";
        private string _sign;
        private string _pdfInstruction = Path.Combine(Application.StartupPath, "Data");

        private Uc_ShowData ucShowData;
        public Main()
        {
            InitializeComponent();
            _sign = "Made by Linh - v" + _ver + ".2025@";
            ShowHomePage();
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
                MessageBox.Show("Tìm Database.", "THÔNG BÁO");
                _url = Helper.GetURLDatabase();
                Application.Restart();
                return;
            }
        }

        private void btnCapNhatMaSP_Click(object sender, EventArgs e)
        {
            resetMainView();
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

            resetMainView();
            string[] dsMay = new[]
            {
                "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10",
                "B11", "B12", "B13", "B14", "B15", "B16", "B17",
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

            ucBen.UcShowDataInstance = ucShowData;
            string filePath = Path.Combine(_pdfInstruction, "01 HD_BEN.pdf");
            ucShowData.LoadPdf(filePath);
        }

        private void resetMainView()
        {
            pnLeft.Controls.Clear();
            pnRight.Controls.Clear();
            pnLeft.Dock = DockStyle.Left;
            pnRight.Dock = DockStyle.Fill;
            pnRight.Visible = true;
        }

        private void btnBocMach_Click(object sender, EventArgs e)
        {
            resetMainView();
            string[] dsMay = new[]
            {
                "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10",
                "E11", "E12", "E13", "E14", "E15"
            };

            string tenCongDoan = "mach";

            ucShowData = Helper.LoadUserControlsWithData<Uc_Boc>(
                pnLeft,
                pnRight,
                out var ucBoc,
                dt => ucShowData.SetData(dt),
                _url, dsMay, tenCongDoan
            );
            ucBoc.LoadDanhSachMay(dsMay);
            ucBoc.TypeOfProduct = "BTP";

            ucBoc.UcShowDataInstance = ucShowData;
            string filePath = Path.Combine(_pdfInstruction, "02 HD_BOC.pdf");
            ucShowData.LoadPdf(filePath);

        }

        private void btnQuanMica_Click(object sender, EventArgs e)
        {
            resetMainView();

            string[] dsMay = new[]
            {
                "T3", "T4", "T5", "T6"
            };

            string tenCongDoan = "mica";

            ucShowData = Helper.LoadUserControlsWithData<Uc_Mica>(
                pnLeft,
                pnRight,
                out var uc_Mica,
                dt => ucShowData.SetData(dt),
                _url, dsMay, tenCongDoan
            );
            uc_Mica.LoadDanhSachMay(dsMay);
            uc_Mica.TypeOfProduct = "BTP";

            uc_Mica.UcShowDataInstance = ucShowData;
            string filePath = Path.Combine(_pdfInstruction, "02 HD_BOC.pdf");
            ucShowData.LoadPdf(filePath);

        }

        private void btnBocVo_Click(object sender, EventArgs e)
        {
            resetMainView();
            string[] dsMay = new[]
            {
                "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10",
                "E11", "E12", "E13", "E14", "E15"
            };

            string tenCongDoan = "vo";

            ucShowData = Helper.LoadUserControlsWithData<Uc_Boc>(
                pnLeft,
                pnRight,
                out var ucBoc,
                dt => ucShowData.SetData(dt),
                _url, dsMay, tenCongDoan
            );
            ucBoc.LoadDanhSachMay(dsMay);
            ucBoc.TypeOfProduct = "TP";


            ucBoc.UcShowDataInstance = ucShowData;
            string filePath = Path.Combine(_pdfInstruction, "02 HD_BOC.pdf");
            ucShowData.LoadPdf(filePath);
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            resetMainView();
            ucShowData = Helper.LoadUserControlsWithData<Uc_BcTonKho>(
                pnLeft,
                pnRight,
                out var ucBcTonKho,
                dt => ucShowData.SetData(dt),
                _url
            );
        }

        private void btnBoSungKL_Click(object sender, EventArgs e)
        {
            resetMainView();
            ucShowData = Helper.LoadUserControlsWithData<Uc_BoSungKL>(
               pnLeft,
               pnRight,
               out var Uc_BoSungKL,
               dt => ucShowData.SetData(dt),
               _url
           );

            Uc_BoSungKL.UcShowDataInstance = ucShowData;
            string filePath = Path.Combine(_pdfInstruction, "03 HD_BO SUNG KL.pdf");
            ucShowData.LoadPdf(filePath);
        }

        private void btnGopBin_Click(object sender, EventArgs e)
        {
            resetMainView();
            ucShowData = Helper.LoadUserControlsWithData<Uc_HanNoi>(
               pnLeft,
               pnRight,
               out var Uc_GopBin,
               dt => ucShowData.SetData(dt),
               _url
           );

            Uc_GopBin.UcShowDataInstance = ucShowData;
            string filePath = Path.Combine(_pdfInstruction, "04 HD_GopBin.pdf");
            ucShowData.LoadPdf(filePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetMainView();
            ucShowData = Helper.LoadUserControlsWithData<Uc_ConfigApp>(
                pnLeft,
                pnRight,
                out var Uc_ConfigApp,
                dt => ucShowData.SetData(dt),
                _url
            );
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowHomePage();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ShowHomePage();
        }

        private void ShowHomePage()
        {
            // Xóa các control cũ trong panel
            pnLeft.Controls.Clear();
            Uc_HomePage homePage = new Uc_HomePage();

            pnLeft.Dock = DockStyle.Fill;
            pnRight.Visible = false;
            pnLeft.Controls.Add(homePage);
            homePage.Dock = DockStyle.Fill;
            homePage.lblVersion.Text = "Phiên bản: " + _ver;
        }
    }
}

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
        private string _sign = "Designed and developed by Linh";
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

        private void btnBen_Click(object sender, EventArgs e)
        {

        }

        private Uc_CapNhatMaSP ucCapNhatMaSP;
        private Uc_ShowData ucShowData;

        private void btnCapNhatMaSP_Click(object sender, EventArgs e)
        {

            ucCapNhatMaSP = new Uc_CapNhatMaSP(_url);
            ucShowData = new Uc_ShowData();

            // Đăng ký sự kiện: khi UserControl1 gửi dữ liệu → chuyển cho UserControl2
            ucCapNhatMaSP.OnDataReady += UcCapNhatMaSP_OnDataReady;

            // Hiển thị
            Helper.LoadUserControl(ucCapNhatMaSP, pnLeft);
            Helper.LoadUserControl(ucShowData, pnRight);

        }
        private void UcCapNhatMaSP_OnDataReady(DataTable dt)
        {
            ucShowData.SetData(dt);
        }
    }
}

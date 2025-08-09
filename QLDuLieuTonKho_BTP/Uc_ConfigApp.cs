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
    public partial class Uc_ConfigApp : UserControl, ICustomUserControl
    {
        private string _url;
        private string _quyen;

        public Uc_ConfigApp(string url)
        {
            InitializeComponent();
            _url = url;
            _quyen = Properties.Settings.Default.UserPass;

            tbQuyenUser.Text = _quyen;
            tbPathDB.Text = _url;
        }

        public event Action<DataTable> OnDataReady;

        private void btnFindPathDB_Click(object sender, EventArgs e)
        {
            if (!Helper.kiemTraPhanQuyen(_quyen)) return;
            _url = Helper.GetURLDatabase();

            MessageBox.Show("Ứng dụng sẽ được khởi động lại để áp dụng thay đổi.", "THÔNG BÁO");
            Application.Restart();
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            string pass = tbQuyenUser.Text.Trim();
            Helper.UpdatePassApp(pass);
        }
    }
}

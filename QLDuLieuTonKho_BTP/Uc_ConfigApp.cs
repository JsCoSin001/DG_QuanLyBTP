using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
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

            DatabaseHelper.SetDatabasePath(url);

            LoadConfigAndShow();
        }

        //if (!Helper.kiemTraPhanQuyen(_quyenMaster)) return;
        public void LoadConfigAndShow()
        {
            try
            {
                ConfigDB config = DatabaseHelper.GetConfig();
                rdActive.Checked = config.Active;
                rdDisActive.Checked = !config.Active;
                rtbMessage.Text = config.Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc dữ liệu từ database!\nChi tiết lỗi: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void rdDisActive_CheckedChanged(object sender, EventArgs e)
        {
            lbThongBao.Visible = rdDisActive.Checked;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!Helper.kiemTraPhanQuyen(_quyen)) return;

            string rtbMsg = rtbMessage.Text.Trim();
            if (rtbMsg.Length == 0 && rdDisActive.Checked)
            {
                DialogResult result = MessageBox.Show(
                    "THÔNG ĐIỆP ĐANG BỊ BỎ TRỐNG, TIẾP TỤC HAY KHÔNG?",
                    "CẢNH BÁO",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Cancel)
                {
                    return; 
                }
            }

            ConfigDB config = new ConfigDB
            {
                Active = rdActive.Checked,
                Message = rtbMsg
            };

            DatabaseHelper.UpdateConfig(config);
        }

        private void rtbMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

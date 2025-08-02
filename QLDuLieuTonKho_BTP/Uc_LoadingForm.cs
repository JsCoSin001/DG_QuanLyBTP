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
    public partial class Uc_LoadingForm : UserControl
    {
        public Uc_LoadingForm(string message)
        {
            InitializeComponent();
            lblThongBao.Text = message; 
        }

        public void UpdateMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => lblThongBao.Text = message));
            }
            else
            {
                lblThongBao.Text = message;
            }
        }

    }
}

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
    public partial class Uc_ShowData : UserControl
    {
        public Uc_ShowData()
        {
            InitializeComponent();
        }
        public void SetData(DataTable dt)
        {
            grDataViewer.DataSource = dt;
        }
    }
}

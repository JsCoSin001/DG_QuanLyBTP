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
    public partial class Uc_GopBin : UserControl
    {
        public Uc_GopBin()
        {
            InitializeComponent();
        }

        private void cbLot_TextUpdate(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            LoadAutoCompleteLot(cbLot.Text);
        }

        private void LoadAutoCompleteLot(string keyword)
        {

            if (string.IsNullOrWhiteSpace(keyword))
            {
                //ResetController_TimLOT();
                cbLot.DroppedDown = false;
                return;
            }

            string para = "Lot";
            string query = @"
                SELECT Lot, KhoiLuongConLai
                FROM TonKho
                WHERE Lot LIKE '%' || @" + para + " || '%' AND KhoiLuongConLai <> 0;";

            DataTable tonKho = DatabaseHelper.GetData(keyword, query, para);

            cbLot.DroppedDown = false;

            cbLot.SelectionChangeCommitted -= cbLot_SelectionChangeCommitted; // tránh trùng event
            // check data return
            if (tonKho.Rows.Count != 0)
            {
                cbLot.DataSource = tonKho;
                cbLot.DisplayMember = "Lot";

                string currentText = keyword;

                cbLot.DroppedDown = true;
                cbLot.Text = currentText;
                cbLot.SelectionStart = cbLot.Text.Length;
                cbLot.SelectionLength = 0;

                cbLot.SelectionChangeCommitted += cbLot_SelectionChangeCommitted;
            }

        }

        private void cbLot_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //ResetController_TimLOT(); 
            decimal klcl = 0;


            if (cbLot.SelectedItem == null || !(cbLot.SelectedItem is DataRowView)) return;

            DataRowView row = (DataRowView)cbLot.SelectedItem;

            klcl = Convert.ToDecimal(row["KhoiLuongConLai"]);

            if (klcl == 0)
            {
                MessageBox.Show("Lot đã hết hàng, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string selectedLot = row["Lot"].ToString();

            string[] result = Helper.PhanTachLot(selectedLot);

            cbLot.DataSource = null;
            cbLot.Text = "";

            if (result.Length < 5)
            {
                MessageBox.Show("Lot không hợp lệ, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //may.Text = result[0];
            //maHT.Text = result[1];
            //STTCD.Text = result[2];
            //sttBin.Value = Convert.ToDecimal(result[3]);
            //soBin.Value = Convert.ToDecimal(result[4]);
            //lot.Text = selectedLot;

            //klTruocBoc.Value = klcl;


            cbLot.Text = "";


        }

    }
}

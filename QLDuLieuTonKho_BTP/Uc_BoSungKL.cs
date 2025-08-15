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
    public partial class Uc_BoSungKL : UserControl, ICustomUserControl
    {
        public event Action<DataTable> OnDataReady;
        public Uc_BoSungKL(string url)
        {
            InitializeComponent();
            Helper.AddHoverEffect(lblHuongDan);
            timer1.Interval = 500;
            DatabaseHelper.SetDatabasePath(url);
        }

        private void cbLot_TextUpdate(object sender, EventArgs e)
        {
            ResetControlerWithoutLot();
            timer1.Stop();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            LoadAutoCompleteTenSP(cbLot.Text);
        }

        private void ResetControlerWithoutLot()
        {
            nmID.Value = 0;
            tbTenSP.Text = "";
            tbTenBin.Text = "";
            nmTongKL.Value = 0;
            nmKLDong.Value = 0;
            nmKLBin.Value = 0;
        }

        private void ResetAllControler()
        {
            ResetControlerWithoutLot();
            cbLot.Text = "";
        }

        private void LoadAutoCompleteTenSP(string keyword)
        {

            // check empty keyword
            if (string.IsNullOrWhiteSpace(keyword))
            {
                ResetControlerWithoutLot();
                cbLot.DroppedDown = false;                
                return;
            }

            string para = "search";
            string query = @"
                    SELECT 
                        DanhSachMaSP.Ten as ten,
                        TonKho.ID AS id,
                        TonKho.Lot as lot
                    FROM DanhSachMaSP
                    JOIN TonKho ON TonKho.MaSP_ID = DanhSachMaSP.ID
                    WHERE TonKho.Lot LIKE '%' || @" + para + @" || '%'
                    AND TonKho.KhoiLuongConLai = 0 And TonKho.HanNoi == 0 AND TonKho.KhoiLuongDauVao = 0
                    ";


            DataTable dslot = DatabaseHelper.GetData(keyword, query, para);

            cbLot.DroppedDown = false;

            cbLot.SelectionChangeCommitted -= tenSP_SelectionChangeCommitted;

            ResetControlerWithoutLot();

            if (dslot.Rows.Count == 0)
            {
                cbLot.DroppedDown = false;
                return;
            }

            cbLot.DataSource = dslot;
            cbLot.DisplayMember = "lot";

            string currentText = keyword;

            cbLot.DroppedDown = true;
            cbLot.Text = currentText;
            cbLot.SelectionStart = cbLot.Text.Length;
            cbLot.SelectionLength = 0;

            cbLot.SelectionChangeCommitted += tenSP_SelectionChangeCommitted;
        }

        private void tenSP_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (cbLot.SelectedItem == null || !(cbLot.SelectedItem is DataRowView)) return;
            DataRowView row = (DataRowView)cbLot.SelectedItem;
            string lot = row["lot"].ToString();
            nmID.Value = Convert.ToDecimal(row["id"]);
            tbTenSP.Text = row["ten"].ToString();
            cbLot.Text = lot;

            string[] temp = Helper.PhanTachLot(lot);

            if (temp.Length != 5) return;

            tbTenBin.Text = temp[temp.Count() - 1];

        }

        private void tbTenBin_TextChanged(object sender, EventArgs e)
        {
            string keyword = tbTenBin.Text;

            if (keyword == "0")
            {
                Console.WriteLine();
                nmKLBin.Value = 0;
                return;
            }


            string para = "TenBin";

            string query = "SELECT KhoiLuongBin FROM DanhSachBin WHERE TenBin = @" + para;

            DataTable klBinDataTable = DatabaseHelper.GetData(keyword, query, para);


            if (klBinDataTable.Rows.Count == 0) return;

            decimal kl = Convert.ToDecimal(klBinDataTable.Rows[0]["KhoiLuongBin"]);
            nmKLBin.Value = kl;
            UpdateKLDong();
        }

        private void UpdateKLDong()
        {
            if (tbTenSP.Text == "")
            {
                MessageBox.Show("Chưa thấy Tên Sản Phẩm, chọn lại số Lot.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongKL = nmTongKL.Value;
            decimal klBin = nmKLBin.Value;

            decimal kLDong = tongKL - klBin;

            if (kLDong < 0) kLDong = 0;
            nmKLDong.Value = kLDong;

        }

        public async void LoadDataAsync()
        {
            // Gọi truy vấn SQL lấy dữ liệu từ TonKho + DanhSachMaSP
            string query = @"
                SELECT 
                    TonKho.ID,
                    DanhSachMaSP.Ten,
                    TonKho.Lot,
                    DanhSachMaSP.Ma,
                    TonKho.KhoiLuongDauVao,
                    TonKho.HanNoi,
                    TonKho.ChieuDai,
                    DanhSachMaSP.KieuSP
                FROM TonKho
                INNER JOIN DanhSachMaSP ON TonKho.MaSP_ID = DanhSachMaSP.ID 
                WHERE TonKho.KhoiLuongConLai = 0 And TonKho.HanNoi == 0 AND TonKho.KhoiLuongDauVao = 0
            ";

            DataTable dt = await Task.Run(() => DatabaseHelper.GetDataFromSQL(query));

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("Không có dữ liệu", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi dữ liệu ra ngoài qua event
            OnDataReady?.Invoke(dt);
        }

        private void tbnLuu_Click(object sender, EventArgs e)
        {
            if (tbTenSP.Text == "")
            {
                MessageBox.Show("Chưa thấy Tên Sản Phẩm, chọn lại số Lot.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id = nmID.Value.ToString();
            decimal kLDong = nmKLDong.Value;
            decimal klBin = nmKLBin.Value;

            if (kLDong == 0 || klBin == 0)
            {
                MessageBox.Show("Kiểm tra lại dữ liệu.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isSuccess = DatabaseHelper.UpdateKhoiLuongVaBin( id, kLDong, tbTenBin.Text, klBin);


            if (isSuccess)
            {
                MessageBox.Show("Thao tác thành công.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAllControler();
            }
            else
            {
                MessageBox.Show("Thao tác thất bại.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void nmTongKL_ValueChanged(object sender, EventArgs e)
        {
            if (nmTongKL.Value > 0) UpdateKLDong();
        }

        private void nmKLBin_ValueChanged(object sender, EventArgs e)
        {
            if (nmKLBin.Value > 0) UpdateKLDong();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ResetAllControler();
        }

        public Uc_ShowData UcShowDataInstance { get; set; }
        private void lblHuongDan_Click(object sender, EventArgs e)
        {
            UcShowDataInstance.ShowHideController(false);
        }

        private void btnXemDS_Click(object sender, EventArgs e)
        {
            LoadDataAsync();
        }
    }
}

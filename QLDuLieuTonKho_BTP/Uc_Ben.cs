using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
using QLDuLieuTonKho_BTP.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QLDuLieuTonKho_BTP.Helper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLDuLieuTonKho_BTP
{

    public partial class Uc_Ben : UserControl, ICustomUserControl
    {
        private string _url;
        private string _titleForm;

        public event Action<DataTable> OnDataReady;

        public string TypeOfProduct { get; set; }
        //public string TitleForm { get; set; }

        public string TitleForm
        {
            get => _titleForm;
            set
            {
                _titleForm = value;
                lblTitleForm.Text = value.ToUpper(); // cập nhật label khi gán
            }
        }

        public Uc_Ben(string url)
        {

            InitializeComponent();

            _url = url;
            DatabaseHelper.SetDatabasePath(url);
            lblTitleForm.Text = _titleForm;

            // Cấu hình timer
            timer1.Interval = 300;
        }


        public Uc_Ben() { }


        public void LoadDanhSachMay(string[] dsMay)
        {
            may.Items.Clear();
            may.Items.AddRange(dsMay);
        }


        private void tbLuu_Click(object sender, EventArgs e)
        {
            Boolean result = false;

            string maBin = lot.Text;
            int maID = (int)idTenSP.Value;
            float kl = (float)khoiLuong.Value;
            float hn = (float)hanNoi.Value;
            float cd = (float)chieuDai.Value;
            string error = "";
            if (maBin == "")
            {
                error = "Kiểm tra lại dữ liệu tại:\n Mã Hành Trình \nSTT Công Đoạn \nSTT Bin \nSố Bin";

                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (maID == 0)
            {
                error = "Tên Sản Phẩm chưa được chọn ";
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                TonKho tonKho = new TonKho
                {
                    Lot = maBin,
                    MaSP_ID = maID,
                    KhoiLuongDauVao = kl,
                    KhoiLuongConLai = kl,
                    HanNoi = hn,
                    ChieuDai = cd
                };

                DL_CD_Ben dL_CD_Ben = new DL_CD_Ben
                {
                    Ngay = ngay.Value.ToString("yyyy-MM-dd"),
                    Ca = ca.Text,
                    NguoiLam = nguoiLam.Text,
                    SoMay = may.Text,
                    GhiChu = ghiChu.Text,
                };

                var validationResults = ValidateInput.ValidateModel(dL_CD_Ben);

                if (validationResults.Count != 0)
                {
                    string errorMessage = string.Join("\n", validationResults.ConvertAll(r => r.ErrorMessage));
                    MessageBox.Show("Lỗi nhập liệu:\n" + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int sttBen = (int)stt.Value;

                if (sttBen == 0)
                    result = DatabaseHelper.InsertSanPhamTonKhoDL<TonKho, DL_CD_Ben>(tonKho, dL_CD_Ben, "dL_CD_Ben");
                else
                {
                    dL_CD_Ben.GhiChu = dL_CD_Ben.GhiChu + "- Đã sửa";
                    result = DatabaseHelper.UpdateDL_CDBen(sttBen, tonKho, dL_CD_Ben);
                }

                ResetAllController();

                if (result) MessageBox.Show("THAO TÁC THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetAllController()
        {
            ngay.Value = DateTime.Now;
            ca.SelectedIndex = -1;
            may.SelectedIndex = -1;

            maHT.Value = 0;
            STTCD.SelectedIndex = -1;
            sttBin.Value = 0;
            soBin.Value = 0;

            lot.Text = "";
            maSP.Text = "";
            idTenSP.Value = 0;
            tenSP.Text = "";
            khoiLuong.Value = 0;
            hanNoi.Value = 0;
            chieuDai.Value = 0;
            nguoiLam.Text = "";
            ghiChu.Text = "";
            stt.Value = 0;
        }

        private void Ben_Load(object sender, EventArgs e)
        {
            //_url = Properties.Settings.Default.URL;
            dateReport.Value = DateTime.Now;

        }

        // Sự kiện khi người dùng thay đổi URL
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); // Ngừng timer để tránh gọi lại liên tục            
            LoadAutoCompleteTenSP(tenSP.Text);
        }

        /// <summary>
        /// Hàm này sẽ tải dữ liệu tự động hoàn thành cho ComboBox tenSP dựa trên từ khóa nhập vào.
        /// </summary>
        /// <param name="keyword"></param>
        private void LoadAutoCompleteTenSP(string keyword)
        {
            // check empty keyword
            if (string.IsNullOrWhiteSpace(keyword))
            {
                idTenSP.Value = 0;
                tenSP.DroppedDown = false;
                maSP.Text = "";
                return;
            }
            string para = "search";

            
            string query = "SELECT ID, Ma, Ten FROM DanhSachMaSP " +
               "WHERE KieuSP = '" + TypeOfProduct + "' " +
               "AND Ten LIKE '%' || @" + para + " || '%' " +
               "AND Ten NOT LIKE '%/T' " +
               "LIMIT 20";

            DataTable dslot = DatabaseHelper.GetData(keyword, query, para);

            tenSP.DroppedDown = false;

            tenSP.SelectionChangeCommitted -= tenSP_SelectionChangeCommitted;

            if (dslot.Rows.Count != 0)
            {
                tenSP.DataSource = dslot;
                tenSP.DisplayMember = "Ten";

                string currentText = keyword;

                tenSP.DroppedDown = true;
                tenSP.Text = currentText;
                tenSP.SelectionStart = tenSP.Text.Length;
                tenSP.SelectionLength = 0;

                tenSP.SelectionChangeCommitted += tenSP_SelectionChangeCommitted;
            }
        }


        private void tenSP_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ResetAllController();

            if (tenSP.SelectedItem == null || !(tenSP.SelectedItem is DataRowView)) return;

            DataRowView row = (DataRowView)tenSP.SelectedItem;

            string ten = row["Ten"].ToString();
            string ma = row["Ma"].ToString();
            decimal id = Convert.ToDecimal(row["ID"]);

            tenSP.Text = ten;
            maSP.Text = ma;
            idTenSP.Value = id;

        }

        private void tenSP_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }
      

        private void tbShowDL_Click(object sender, EventArgs e)
        {
            int id_MaSP = (int)stt.Value;
            string query = @"
                SELECT 
                    DL_CD_Ben.*,
                    DanhSachMaSP.id AS idTenSP,
                    DanhSachMaSP.Ma AS MaSP,
                    DanhSachMaSP.Ten AS TenSP,
                    TonKho.Lot,
                    TonKho.KhoiLuongDauVao,
                    TonKho.HanNoi,
                    TonKho.ChieuDai
                FROM 
                    DL_CD_Ben
                INNER JOIN 
                    TonKho ON DL_CD_Ben.TonKho_ID = TonKho.ID
                INNER JOIN 
                    DanhSachMaSP ON TonKho.MaSP_ID = DanhSachMaSP.ID
                WHERE 
                    DL_CD_Ben.ID = @ID";

            DataTable data = DatabaseHelper.GetDL_CDBenByID(id_MaSP, query);

            // check data return
            if (data.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu với ID được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dataRow = data.Rows[0];

            string maBin = dataRow["lot"].ToString();
            string[] result = Helper.PhanTachLot(maBin);
            may.Text = result[0];
            maHT.Text = result[1];
            STTCD.Text = result[2];
            sttBin.Value = Convert.ToDecimal(result[3]);
            soBin.Value = Convert.ToDecimal(result[4]);

            ngay.Text = dataRow["ngay"].ToString();
            ca.Text = dataRow["ca"].ToString();
            may.Text = dataRow["soMay"].ToString();
            lot.Text = maBin;
            maSP.Text = dataRow["MaSP"].ToString();
            idTenSP.Value = Convert.ToDecimal(dataRow["idTenSP"]);
            tenSP.Text = dataRow["TenSP"].ToString();
            khoiLuong.Value = Convert.ToDecimal(dataRow["KhoiLuongDauVao"]);
            hanNoi.Value = Convert.ToDecimal(dataRow["HanNoi"]);
            chieuDai.Value = Convert.ToDecimal(dataRow["ChieuDai"]);
            nguoiLam.Text = dataRow["NguoiLam"].ToString();
            ghiChu.Text = dataRow["ghiChu"].ToString();

        }

        private void may_SelectedIndexChanged(object sender, EventArgs e)
        {
            lot.Text = Helper.LOTGenerated(may, maHT, STTCD, sttBin, soBin);
        }

        private void STTCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lot.Text = Helper.LOTGenerated(may, maHT, STTCD, sttBin, soBin);
        }

        private void maHT_ValueChanged(object sender, EventArgs e)
        {
            lot.Text = Helper.LOTGenerated(may, maHT, STTCD, sttBin, soBin);
        }

        private void sttBin_ValueChanged(object sender, EventArgs e)
        {
            lot.Text = Helper.LOTGenerated(may, maHT, STTCD, sttBin, soBin);
        }

        private void soBin_ValueChanged(object sender, EventArgs e)
        {
            lot.Text = Helper.LOTGenerated(may, maHT, STTCD, sttBin, soBin);
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            ResetAllController();
        }

        private async void showReport_Click(object sender, EventArgs e)
        {
            string dateRP = dateReport.Value.Date.ToString("yyyy-MM");

            string query = @"
                    SELECT 
                        DL_CD_Ben.ID,
                        DL_CD_Ben.Ngay,
                        TonKho.Lot as LOT,
                        DL_CD_Ben.Ca,
                        DL_CD_Ben.NguoiLam,
                        DanhSachMaSP.Ma,
                        DanhSachMaSP.Ten,
                        TonKho.KhoiLuongDauVao,
                        TonKho.KhoiLuongConLai,
                        TonKho.HanNoi,
                        TonKho.ChieuDai,
                        DL_CD_Ben.SoMay,
                        DL_CD_Ben.GhiChu
                    FROM DL_CD_Ben
                    JOIN TonKho ON DL_CD_Ben.TonKho_ID = TonKho.ID
                    JOIN DanhSachMaSP ON TonKho.MaSP_ID = DanhSachMaSP.ID
                    WHERE strftime('%Y-%m', DL_CD_Ben.Ngay) = @Ngay
                    ORDER BY DL_CD_Ben.Ngay DESC;
                ";

            DataTable table = DatabaseHelper.GetDataByDate(dateRP, query);

            if (!cbXuatExcel.Checked)
            {
                OnDataReady?.Invoke(table);
                return;
            }

            string fileName = $"BC Tháng {dateRP} - {DateTime.Now:yyyy-MM-dd HH_mm}.xlsx";
            await ExcelHelper.ExportWithLoading(table, fileName);



           
        }

    }
}

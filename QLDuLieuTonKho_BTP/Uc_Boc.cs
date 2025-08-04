using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
using QLDuLieuTonKho_BTP.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace QLDuLieuTonKho_BTP
{

    public partial class Uc_Boc : UserControl, ICustomUserControl
    {

        private string _url;
        bool isProgrammaticChange = true;
        private string[] _dsMay;

        public event Action<DataTable> OnDataReady;

        public Uc_Boc(string url, string[] dsMay)
        {

            InitializeComponent();

            _url = url;
            _dsMay = dsMay;

            DatabaseHelper.SetDatabasePath(url);

            // Cấu hình timer
            timer1.Interval = 300;
        }

        public Uc_Boc() { }

        public void LoadDanhSachMay(string[] dsMay)
        {
            may.Items.Clear();
            may.Items.AddRange(dsMay);
        }

        public string TypeOfProduct { get; set; }

        private void tbLuu_Click(object sender, EventArgs e)
        {

            string maBin = lot.Text;
            int maID = (int)idTenSP.Value;
            float klTB = (float)klTruocBoc.Value;
            float klCL = (float)klConLai.Value;
            float klP = (float)klPhe.Value;
            float cd = (float)chieuDai.Value;
            string tenCongDoan = congDoan.Text;

            string error = "";

            if (maBin == "" || tenCongDoan == "")
            {
                error = "Kiểm tra lại dữ liệu tại:\nMã Hành Trình \nSTT Công Đoạn \nSTT Bin \nSố Bin";

                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (maID == 0)
            {
                error = "Tên Sản Phẩm chưa được chọn ";
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (klTB == 0)
            {
                error = "Lot không tồn tại hoặc đã hết";
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Biến chứa dữ liệu up mới vào tồn kho
                TonKho tonKhoMoi = new TonKho
                {
                    Lot = Helper.GenerateRandomString("Boc"),
                    MaSP_ID = maID,
                    KhoiLuongDauVao = klTB,
                    KhoiLuongConLai = klTB,
                    HanNoi = 0,
                    ChieuDai = cd
                };

                DL_CD_Boc dL_CD_Boc = new DL_CD_Boc
                {
                    Ngay = ngay.Value.ToString("yyyy-MM-dd"),
                    Ca = ca.Text,
                    KhoiLuongTruocBoc = klTB,
                    KhoiLuongPhe = double.TryParse(klPhe.Text, out var phe) ? phe : 0,
                    NguoiLam = nguoiLam.Text,
                    SoMay = maySX.Text,
                    TenCongDoan = tenCongDoan,
                    GhiChu = ghiChu.Text,
                    MaSP_ID = (int)idTenSP.Value,
                    CD_Ben_ID = (int)idBen.Value
                };

                //Biến chứa dữ liệu update tồn kho
                TonKho tonKho_update = new TonKho
                {
                    Lot = maBin,
                    KhoiLuongConLai = klCL
                };


                var validationResults = ValidateInput.ValidateModel(dL_CD_Boc);

                if (validationResults.Count != 0)
                {
                    string errorMessage = string.Join("\n", validationResults.ConvertAll(r => r.ErrorMessage));
                    MessageBox.Show("Lỗi nhập liệu:\n" + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int sttB = (int)stt.Value;
                Boolean result = false;

                if (sttB == 0)
                {
                    // Thêm mới lot vào database công đoạn bọc
                    result = DatabaseHelper.InsertSanPhamTonKhoDL<TonKho, DL_CD_Boc>(tonKhoMoi, dL_CD_Boc, "DL_CD_Boc");

                    // Update số lượng tồn kho
                    if (result) result = DatabaseHelper.UpdateTonKho_SLConLaiThucTe(tonKho_update);
                }
                else
                {
                    dL_CD_Boc.GhiChu = dL_CD_Boc.GhiChu + "- Đã sửa";
                    result = DatabaseHelper.UpdateDL_CDBoc(sttB, tonKhoMoi, dL_CD_Boc);
                }

                ResetController();
                if (result) MessageBox.Show("THAO TÁC THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetController()
        {
            ngay.Value = DateTime.Now;
            ca.SelectedIndex = -1;
            may.SelectedIndex = -1;

            maHT.Value = 0;
            STTCD.SelectedIndex = -1;
            sttBin.Value = 0;
            soBin.Value = 0;
            congDoan.SelectedIndex = -1;
            maySX.SelectedIndex = -1;

            lot.Text = "";
            maSP.Text = "";
            idTenSP.Value = 0;
            tenSP.Text = "";
            klTruocBoc.Value = 0;
            klPhe.Value = 0;
            chieuDai.Value = 0;
            klConLai.Value = 0;
            nguoiLam.Text = "";
            ghiChu.Text = "";
            stt.Value = 0;
        }

        private void Boc_Load(object sender, EventArgs e)
        {
            dateReport.Value = DateTime.Now;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); // Ngừng timer để tránh gọi lại liên tục            
            LoadAutoCompleteData(tenSP.Text);
        }

        private void LoadAutoCompleteData(string keyword)
        {
            // check empty keyword
            if (string.IsNullOrWhiteSpace(keyword))
            {
                idTenSP.Value = 0;
                maSP.Text = "";
                congDoan.Text = "";
                tenSP.DataSource = null;
                tenSP.Items.Clear();
                return;
            }

            if (congDoan.SelectedIndex < 0)
            {
                string error = "Cần lựa chọn Tên Công Đoạn trước!";
                MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tenSP.Text = "";
                return;
            }

            string query = "SELECT ID, Ma, Ten FROM DanhSachMaSP WHERE KieuSP = '" + TypeOfProduct + "' AND Ten LIKE '%' || @search || '%' LIMIT 20";

            List<ProductModel> names = DatabaseHelper.GetProductNamesAndPartNumber(query, keyword);

            Helper.UpdateComboBox(names, tenSP, maSP, idTenSP);
        }

        private void tenSP_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void tbShowDL_Click(object sender, EventArgs e)
        {
            isProgrammaticChange = false;

            int id_MaSP = (int)stt.Value;
            string query = @"
                SELECT 
                    DL_CD_Boc.*,
                    DanhSachMaSP.id AS idTenSP,
                    DanhSachMaSP.Ma AS MaSP,
                    DanhSachMaSP.Ten AS TenSP,
                    DL_CD_Boc.KhoiLuongTruocBoc,
                    TonKho.ChieuDai,
                    TonKho_Ben.Lot AS lot,
                    DL_CD_Boc.cd_ben_id as idCDBen,
                    TonKho_Ben.KhoiLuongConLai AS KhoiLuongConLai
                FROM 
                    DL_CD_Boc
                INNER JOIN 
                    TonKho ON DL_CD_Boc.TonKho_ID = TonKho.ID
                INNER JOIN 
                    DanhSachMaSP ON TonKho.MaSP_ID = DanhSachMaSP.ID
                LEFT JOIN 
                    DL_CD_Ben ON DL_CD_Boc.CD_Ben_ID = DL_CD_Ben.ID
                LEFT JOIN 
                    TonKho AS TonKho_Ben ON DL_CD_Ben.TonKho_ID = TonKho_Ben.ID
                WHERE 
                    DL_CD_Boc.ID = @ID";

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
            lot.Text = maBin;

            ngay.Text = dataRow["ngay"].ToString();
            ca.Text = dataRow["ca"].ToString();
            maySX.Text = dataRow["soMay"].ToString();
            maSP.Text = dataRow["MaSP"].ToString();
            idTenSP.Value = Convert.ToDecimal(dataRow["idTenSP"]);
            tenSP.Text = dataRow["TenSP"].ToString();
            klTruocBoc.Value = Convert.ToDecimal(dataRow["KhoiLuongTruocBoc"]);
            congDoan.Text = dataRow["TenCongDoan"].ToString();
            klConLai.Value = Convert.ToDecimal(dataRow["KhoiLuongConLai"]);
            klPhe.Value = Convert.ToDecimal(dataRow["KhoiLuongPhe"]);
            chieuDai.Value = Convert.ToDecimal(dataRow["ChieuDai"]);
            nguoiLam.Text = dataRow["NguoiLam"].ToString();
            ghiChu.Text = dataRow["ghiChu"].ToString();
            idBen.Value = Convert.ToDecimal(dataRow["cd_ben_id"]);

            isProgrammaticChange = true;
        }                

        private void may_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange) Helper.RunEvent(may, maHT, STTCD, sttBin, soBin, lot, idBen, klTruocBoc);
        }

        private void STTCD_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (isProgrammaticChange) Helper.RunEvent(may, maHT, STTCD, sttBin, soBin, lot, idBen, klTruocBoc);
        }

        private void maHT_ValueChanged(object sender, EventArgs e)
        {

            if (isProgrammaticChange) Helper.RunEvent(may, maHT, STTCD, sttBin, soBin, lot, idBen, klTruocBoc);
        }

        private void sttBin_ValueChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange) Helper.RunEvent(may, maHT, STTCD, sttBin, soBin, lot, idBen, klTruocBoc);
        }

        private void soBin_ValueChanged(object sender, EventArgs e)
        {

            if (isProgrammaticChange) Helper.RunEvent(may, maHT, STTCD, sttBin, soBin, lot, idBen, klTruocBoc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetController();
        }

        private void showReport_Click_1(object sender, EventArgs e)
        {
            string dateRP = dateReport.Value.Date.ToString("yyyy-MM");

            string query = @"
                SELECT 
                    DL_CD_Boc.ID,
                    DL_CD_Boc.Ngay,
                    DL_CD_Boc.NguoiLam,
                    DanhSachMaSP.Ten as TenSP,
                    DL_CD_Boc.TenCongDoan as CongDoan,
                    DL_CD_Boc.Ca,
                    DL_CD_Boc.SoMay,
                    DL_CD_Boc.KhoiLuongTruocBoc,
                    TonKho_Ben.KhoiLuongConLai,
                    TonKho.ChieuDai,
                    DL_CD_Boc.KhoiLuongPhe,
                    DL_CD_Boc.GhiChu
               FROM 
                    DL_CD_Boc
                INNER JOIN 
                    TonKho ON DL_CD_Boc.TonKho_ID = TonKho.ID
                INNER JOIN 
                    DanhSachMaSP ON TonKho.MaSP_ID = DanhSachMaSP.ID
                LEFT JOIN 
                    DL_CD_Ben ON DL_CD_Boc.CD_Ben_ID = DL_CD_Ben.ID
                LEFT JOIN 
                    TonKho AS TonKho_Ben ON DL_CD_Ben.TonKho_ID = TonKho_Ben.ID
                WHERE strftime('%Y-%m', DL_CD_Boc.Ngay) = @Ngay
                ORDER BY DL_CD_Boc.Ngay DESC;
            ";


            DataTable table = DatabaseHelper.GetDataByDate(dateRP, query);

            OnDataReady?.Invoke(table);
        }
    }
}

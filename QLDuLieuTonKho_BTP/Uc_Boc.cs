using DocumentFormat.OpenXml.Spreadsheet;
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
        bool isProgrammaticChange = true;
        //private string[] _dsMay;
        private string _callTimer;

        // Đơn vị kg
        private decimal _litmitTrash = 5;
        public event Action<DataTable> OnDataReady;
        private static readonly string _quyenMaster = Properties.Settings.Default.UserPass;

        private Dictionary<string, string> dsCongDoan = new Dictionary<string, string>()
        {
            {"mica", "Quấn Mica"},
            {"mach", "Bọc Mạch"},
            {"vo", "Bọc Vỏ"}
        };
        
        public Uc_Boc(string url, string[] dsMay, string selectedCD)
        {
            InitializeComponent();

            Helper.AddHoverEffect(lbHuongDan);

            congDoan.DataSource = new BindingSource(dsCongDoan, null);
            congDoan.DisplayMember = "Value"; // Hiển thị text cho user
            congDoan.ValueMember = "Key";

            string tenCD = dsCongDoan[selectedCD];

            congDoan.SelectedValue = selectedCD;

            DatabaseHelper.SetDatabasePath(url);
            lblTitleForm.Text = ("BÁO cáo công đoạn " + tenCD).ToUpper();

            // Cấu hình timer
            timer1.Interval = 300;

            ngay.Value = DateTime.Parse(Helper.GetNgayHienTai());
            ca.Text = Helper.GetShiftValue();
        }

        public Uc_Boc() { }

        public void LoadDanhSachMay(string[] dsMay)
        {
            maySX.Items.Clear();
            maySX.Items.AddRange(dsMay);
        }

        public string TypeOfProduct { get; set; }
        public string TenCongDoan { get; set; }

        private async void tbLuu_Click(object sender, EventArgs e)
        {
            try
            {
                tbLuu.Enabled = false;
                string maBin = lot.Text;
                int maID = (int)idTenSP.Value;
                decimal klTB = (decimal)klTruocBoc.Value;
                decimal klCL = (decimal)klConLai.Value;
                decimal klP = (decimal)klPhe.Value;
                decimal cd = (decimal)chieuDai.Value;
                string tenCongDoan = congDoan.Text;
                int id_cd_ben = (int)idBen.Value;

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

                if (klTB < klCL + klP)
                {
                    error = "Kiểm tra lại Khối Lượng Phế hoặc Khối Lượng Còn Lại";
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (id_cd_ben == 0)
                {
                    error = "Dữ liệu bất thường. Hãy kiểm tra và nhập lại";
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // Biến chứa dữ liệu up mới vào tồn kho
                    TonKho tonKhoMoi = new TonKho
                    {
                        Lot = Helper.GenerateRandomString(congDoan.SelectedValue.ToString()),
                        MaSP_ID = maID,
                        KhoiLuongDauVao = klTB,
                        KhoiLuongConLai = klTB,
                        //HanNoi = 0,
                        ChieuDai = cd
                    };

                    DL_CD_Boc dL_CD_Boc = new DL_CD_Boc
                    {
                        Ngay = Helper.GetNgayHienTai(),
                        Ca = ca.Text,
                        KhoiLuongTruocBoc = klTB,
                        KhoiLuongConLai = klCL,
                        KhoiLuongPhe = klPhe.Value,
                        NguoiLam = nguoiLam.Text,
                        SoMay = maySX.Text,
                        TenCongDoan = tenCongDoan,
                        GhiChu = ghiChu.Text,
                        MaSP_ID = (int)idTenSP.Value,
                        CD_Ben_ID = (int)idBen.Value
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

                    // Gửi email nếu khối lượng phế lớn
                    if (Helper.needToSendEmail(dL_CD_Boc.KhoiLuongPhe, dL_CD_Boc.KhoiLuongTruocBoc))
                    {
                        //var recipients = new List<string> {
                        //        "cosin.js@gmail.com" ,
                        //        "Trongsao2024@gmail.com",
                        //        "quynhdg76@gmail.com",
                        //        "thuantq@ngockhanh.vn",
                        //        "thanhdu@donggiang.vn",
                        //        "thangnguyencz@gmail.com",
                        //        "thongtv@ngockhanh.vn",
                        //        "sondv@donggiang.vn",
                        //};

                        var recipients = new List<string> {
                                "cosin.js@gmail.com" ,
                                
                        };

                        decimal tyLePhe = dL_CD_Boc.KhoiLuongTruocBoc == 0
                                ? 0m
                                : Math.Round(klP * 100m / dL_CD_Boc.KhoiLuongTruocBoc, 2, MidpointRounding.AwayFromZero);


                        string nd =
                        $@"
                        <div style=""font-family:Segoe UI,Arial,sans-serif;font-size:14px;line-height:1.6;color:#111;"">
                            <p>[Email tự động - Không trả lời email này]</p>
                            <p><strong>- Ngày:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                            <p><strong>- Công đoạn:</strong> {tenCongDoan}</p>
                            <p><strong>- Lot bện:</strong> {maBin}</p>
                            <p><strong>- Mã SP:</strong> {maSP.Text}</p>
                            <p><strong>- Tên SP:</strong> {tenSP.Text}</p>
                            <p><strong>- Lot {tenCongDoan}:</strong> {tonKhoMoi.Lot}</p>
                            <p><strong>- KL đầu vào:</strong> {dL_CD_Boc.KhoiLuongTruocBoc} kg</p>
                            <p><strong>- KL phế:</strong> {klP} kg</p>
                            <p><strong>- Tỷ lệ phế:</strong> {tyLePhe.ToString("0.00")} %</p>
                            <p><strong>- Người làm:</strong> {nguoiLam.Text}</p>
                            <p><strong>- Ghi chú:</strong> {ghiChu.Text}</p>
                        </div>";

                        _ = Task.Run(() => SendEmailHelper.SendEmail(recipients, nd, useBcc: true));
                    }


                    if (sttB == 0)
                    {

                        //Biến chứa dữ liệu update tồn kho
                        TonKho tonKho_update = new TonKho
                        {
                            Lot = maBin,
                            KhoiLuongConLai = klCL
                        };

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

                    ResetAllController();
                    if (result) MessageBox.Show("THAO TÁC THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                tbLuu.Enabled = true;
            }
        }

        private void ResetAllController()
        {
            ngay.Value = DateTime.Parse(Helper.GetNgayHienTai());
            ca.Text = Helper.GetShiftValue();
            may.SelectedIndex = -1;

            maHT.Value = 0;
            STTCD.SelectedIndex = -1;
            sttBin.Value = 0;
            soBin.Value = 0;
            //congDoan.SelectedIndex = -1;
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

            //cbTimLot.DataSource = null;
            cbTimLot.Text = "";

            //tenSP.DataSource = null;
            tenSP.Text = "";
        }

        private void ResetController_TimLOT()
        {
            may.SelectedIndex = -1;
            maHT.Value = 0;
            STTCD.SelectedIndex = -1;
            sttBin.Value = 0;
            soBin.Value = 0;
            lot.Clear();
        }

        private void ResetController_TimTenSP()
        {            
            maSP.Text = "";
            idTenSP.Value = 0;
            tenSP.Text = "";
        }

        private void Boc_Load(object sender, EventArgs e)
        {
            dateReport.Value = DateTime.Parse(Helper.GetNgayHienTai());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();   

            if (_callTimer == "cbTimLot")
            {
                LoadAutoCompleteLot(cbTimLot.Text);
                return;
            }

            LoadAutoCompleteTenSP(tenSP.Text);
        }

        private void LoadAutoCompleteLot(string keyword)
        {

            if (string.IsNullOrWhiteSpace(keyword))
            {
                ResetController_TimLOT();
                cbTimLot.DroppedDown = false;
                return;
            }
            string para = "Lot";

            string query = @"
                SELECT b.ID, t.Lot, t.KhoiLuongConLai
                FROM DL_CD_Ben b
                JOIN TonKho t ON b.TonKho_ID = t.ID
                WHERE t.Lot LIKE '%' || @" + para + @" || '%'
                  AND t.KhoiLuongConLai <> 0
                  AND t.Lot NOT LIKE 'Z_%';";



            DataTable tonKho = DatabaseHelper.GetData( keyword, query,para);

            cbTimLot.DroppedDown = false;

            cbTimLot.SelectionChangeCommitted -= cbTimLot_SelectionChangeCommitted; // tránh trùng event
            // check data return
            if (tonKho.Rows.Count == 0) return;
            
            cbTimLot.DataSource = tonKho;
            cbTimLot.DisplayMember = "Lot";

            string currentText = keyword;

            cbTimLot.DroppedDown = true;
            cbTimLot.Text = currentText;
            cbTimLot.SelectionStart = cbTimLot.Text.Length;
            cbTimLot.SelectionLength = 0;

            cbTimLot.SelectionChangeCommitted += cbTimLot_SelectionChangeCommitted;
                    

        }

        private void cbTimLot_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ResetController_TimLOT();
            decimal klcl = 0;


            if (cbTimLot.SelectedItem == null || !(cbTimLot.SelectedItem is DataRowView)) return;

            DataRowView row = (DataRowView)cbTimLot.SelectedItem;

            klcl = Convert.ToDecimal(row["KhoiLuongConLai"]);

            if (klcl == 0)
            {
                MessageBox.Show("Lot đã hết hàng, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string selectedLot =  row["Lot"].ToString();

            string[] result = Helper.PhanTachLot(selectedLot);

            cbTimLot.Text = "";

            if (result.Length == 5)
            {
                try
                {
                    may.Text = result[0];
                    maHT.Text = result[1];
                    STTCD.Text = result[2];
                    sttBin.Value = Convert.ToDecimal(result[3]);
                    soBin.Value = Convert.ToDecimal(result[4]);
                }
                catch (Exception){}
                
            }
            
            lot.Text = selectedLot;

            klTruocBoc.Value = klcl;
            idBen.Value = Convert.ToDecimal(row["ID"]);

            cbTimLot.Text = "";
        }

        private void LoadAutoCompleteTenSP(string keyword)
        {

            // check empty keyword
            if (string.IsNullOrWhiteSpace(keyword))
            {
                ResetController_TimTenSP();
                tenSP.DroppedDown = false;
                return;
            }
            
            string para = "search";
            string query = "SELECT ID, Ma, Ten FROM DanhSachMaSP WHERE  KieuSP = '" + TypeOfProduct + "' AND Ten LIKE '%' || @"+para+" || '%' ";

            int idCongDoan = congDoan.SelectedIndex;

            if (idCongDoan == 0)            
                query += " AND (Ten LIKE 'CM%') ";            
            else            
                query += " AND (Ten LIKE 'CE%' OR Ten LIKE 'CV%' OR Ten LIKE 'AE%' OR Ten LIKE 'AV%') ";
            
            //query += " LIMIT 20";

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
            ResetController_TimTenSP();

            if (tenSP.SelectedItem == null || !(tenSP.SelectedItem is DataRowView)) return;

            DataRowView row = (DataRowView)tenSP.SelectedItem;

            string ten = row["Ten"].ToString();
            string ma = row["Ma"].ToString();
            decimal id = Convert.ToDecimal(row["ID"]);

            tenSP.Text = ten;
            maSP.Text = ma;
            idTenSP.Value = id;

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
                    DL_CD_Boc.KhoiLuongConLai AS KhoiLuongConLai
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


            if (result.Length == 5)
            {
                try
                {
                    may.Text = result[0];
                    maHT.Text = result[1];
                    STTCD.Text = result[2];
                    sttBin.Value = Convert.ToDecimal(result[3]);
                    soBin.Value = Convert.ToDecimal(result[4]);
                }
                catch (Exception) { }

            }

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

        private void button1_Click(object sender, EventArgs e)
        {
            ResetAllController();
        }

        private async void showReport_Click_1(object sender, EventArgs e)
        {
            string dateRP = dateReport.Value.Date.ToString("yyyy-MM");

            string query = $@"
                SELECT
                    b.ID AS STT,
                    b.Ngay,
                    tk_ben.Lot AS Lot_Ben,
                    sp.Ten AS TenSP,
                    b.NguoiLam,
                    b.TenCongDoan AS CongDoan,
                    b.Ca,
                    b.SoMay,
                    b.KhoiLuongTruocBoc,
                    b.KhoiLuongConLai,
                    tk_boc.ChieuDai,
                    b.KhoiLuongPhe,
                    b.GhiChu
                FROM DL_CD_Boc AS b
                INNER JOIN TonKho AS tk_boc
                        ON b.TonKho_ID = tk_boc.ID
                INNER JOIN DanhSachMaSP AS sp
                        ON tk_boc.MaSP_ID = sp.ID
                LEFT JOIN DL_CD_Ben AS ben
                       ON b.CD_Ben_ID = ben.ID
                LEFT JOIN TonKho AS tk_ben
                       ON ben.TonKho_ID = tk_ben.ID
                WHERE strftime('%Y-%m', b.Ngay) = @Ngay 
                  AND b.TenCongDoan = '{congDoan.Text}'
                ORDER BY b.ID DESC;
            ";


            DataTable table = DatabaseHelper.GetDataByDate(dateRP, query);

            if (table.Rows.Count < 1)
            {
                MessageBox.Show("Không có dữ liệu", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!cbXuatExcel.Checked)
            {
                OnDataReady?.Invoke(table);
                return;
            }


            cbXuatExcel.Checked = false;
            if (!Helper.kiemTraPhanQuyen(_quyenMaster)) return;
            cbXuatExcel.Checked = true;

            string tenCD = "";

            if (congDoan.SelectedIndex != -1)
            {
                tenCD = congDoan.Text;
            }

            string fileName = $"BC Tháng {dateRP} - CĐ {tenCD}.xlsx";
            await ExcelHelper.ExportWithLoading(table, fileName);
        }

        private void tenSP_TextUpdate(object sender, EventArgs e)
        {
            _callTimer = "tenSP";
            timer1.Stop();
            timer1.Start();
        }

        private void cbTimLot_TextUpdate(object sender, EventArgs e)
        {
            _callTimer = "cbTimLot";
            timer1.Stop();
            timer1.Start();
        }

        public Uc_ShowData UcShowDataInstance { get; set; }
        
        private void lbHuongDan_Click(object sender, EventArgs e)
        {
            UcShowDataInstance.ShowHideController(false);
        }
    }
}

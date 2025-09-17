using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
using QLDuLieuTonKho_BTP.Validate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QLDuLieuTonKho_BTP.Helper;

namespace QLDuLieuTonKho_BTP
{

    public partial class Uc_Mica : UserControl, ICustomUserControl
    {
        bool isProgrammaticChange = true;
        //private string[] _dsMay;
        private string _callTimer;

        // Đơn vị kg
        public event Action<DataTable> OnDataReady;
        private static readonly string _quyenMaster = Properties.Settings.Default.UserPass;

        private Dictionary<string, string> dsCongDoan = new Dictionary<string, string>()
        {
            {"mica", "Quấn Mica"},
            {"mach", "Bọc Mạch"},
            {"vo", "Bọc Vỏ"}
        };
        
        public Uc_Mica(string url, string[] dsMay, string selectedCD)
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

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "btnDelete";
            btnDelete.HeaderText = "Xóa";
            btnDelete.Text = "X";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgDsLot.Columns.Add(btnDelete);
            dgDsLot.AllowUserToAddRows = false;
            //dgDsLot.CellContentClick += dgDsLot_CellContentClick; // sự kiện cho nút
            dgDsLot.CellClick += dgDsLot_CellClick;               // tùy chọn: nếu muốn bắt click cả ô

            dgDsLot.Columns.Add("ID", "ID");
            dgDsLot.Columns.Add("lot", "Lô");
            dgDsLot.Columns.Add("conLai", "Còn Lại");
            dgDsLot.Columns.Add("ten", "Tên sản phẩm");
            dgDsLot.Columns.Add("kl", "Khối lượng");

            dgDsLot.Columns["ID"].Width = 50;
            dgDsLot.Columns["ten"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDsLot.Columns["lot"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDsLot.Columns["conLai"].Width = 70;


            //dgDsLot.Columns["ten"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgDsLot.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgDsLot.Columns["lot"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // ===== Thêm cột nút Xóa =====

        }

        public Uc_Mica() { }

        public void LoadDanhSachMay(string[] dsMay)
        {
            maySX.Items.Clear();
            maySX.Items.AddRange(dsMay);
        }

        public string TypeOfProduct { get; set; }
        public string TenCongDoan { get; set; }

        private void tbLuu_Click(object sender, EventArgs e)
        {
            try
            {
                tbLuu.Enabled = false;
                //string maBin = lot.Text;
                int maID = (int)idTenSP.Value;
                decimal klP = (decimal)klPhe.Value;
                decimal cd = (decimal)chieuDai.Value;
                string tenCongDoan = congDoan.Text;

                string error = "";

                //Biến chứa dữ liệu update tồn kho
                List<TonKho> tonKho_update = new List<TonKho>();
                decimal klTB = 0;
                decimal klconlai = 0;



                if (dgDsLot.Rows.Count == 0)
                {
                    error = "Kiểm tra lại DS Lô đầu vào";

                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {

                    foreach (DataGridViewRow row in dgDsLot.Rows)
                    {
                        TonKho tk = new TonKho();

                        tk.ID = int.Parse(row.Cells[1].Value.ToString());
                        tk.KhoiLuongConLai = Convert.ToDecimal(row.Cells[3].Value);

                        tonKho_update.Add(tk);

                        klTB += Convert.ToDecimal(row.Cells[5].Value);
                        klconlai += Convert.ToDecimal(row.Cells[3].Value);
                    }
                }

                klTB = klTB - klconlai;

                int sttB = (int)stt.Value;

                if (sttB > 0) klTB = Convert.ToDecimal(tbKhoiLuongTruocBoc.Text);


                if (maID == 0)
                {
                    error = "Tên Sản Phẩm chưa được chọn ";
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (klTB <= 0)
                {
                    error = "Dữ liệu không hợp lệ";
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
                        ChieuDai = cd
                    };

                    DL_CD_Boc dL_CD_Boc = new DL_CD_Boc
                    {
                        Ngay = Helper.GetNgayHienTai(),
                        Ca = ca.Text,
                        KhoiLuongTruocBoc = klTB,
                        KhoiLuongConLai = klTB,
                        KhoiLuongPhe = klPhe.Value,
                        NguoiLam = nguoiLam.Text,
                        SoMay = maySX.Text,
                        TenCongDoan = tenCongDoan,
                        GhiChu = ghiChu.Text,
                        MaSP_ID = (int)idTenSP.Value,
                    };

                    var validationResults = ValidateInput.ValidateModel(dL_CD_Boc);

                    if (validationResults.Count != 0)
                    {
                        string errorMessage = string.Join("\n", validationResults.ConvertAll(r => r.ErrorMessage));
                        MessageBox.Show("Lỗi nhập liệu:\n" + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Boolean result = false;

                    decimal tyLePhe = klP / klTB;

                    bool sendEmail = Helper.needToSendEmail(tyLePhe,"nhua");

                    if (sendEmail)
                    {
                        List<string> recipients = Helper.GetDSNguoiNhan();
                        string tlPhe = Math.Round(tyLePhe * 100, 2).ToString("0.00");

                        if (recipients.Count != 0)
                        {
                            string nd =
                                    $@"
                                    <div style=""font-family:Segoe UI,Arial,sans-serif;font-size:14px;line-height:1.6;color:#111;"">
                                        <p>[Email tự động - Không trả lời email này]</p>
                                        <p><strong>- Ngày:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                                        <p><strong>- Công đoạn:</strong> {tenCongDoan}</p>
                                        <p><strong>- Mã SP:</strong> {maSP.Text}</p>
                                        <p><strong>- Tên SP:</strong> {tenSP.Text}</p>
                                        <p><strong>- Lot {tenCongDoan}:</strong> {tonKhoMoi.Lot}</p>
                                        <p><strong>- KL đầu vào:</strong> {dL_CD_Boc.KhoiLuongTruocBoc} kg</p>
                                        <p><strong>- KL còn lại:</strong> {dL_CD_Boc.KhoiLuongConLai} kg</p>
                                        <p><strong>- KL phế:</strong> {klP} kg</p>
                                        <p><strong>- Tỷ lệ phế:</strong> {(tyLePhe * 100m).ToString("0.00")} %</p>
                                        <p><strong>- Người làm:</strong> {nguoiLam.Text}</p>
                                        <p><strong>- Ghi chú:</strong> {ghiChu.Text}</p>
                                    </div>";

                            _ = Task.Run(() => SendEmailHelper.SendEmail(recipients, nd, useBcc: true));

                        }
                        else
                        {
                            MessageBox.Show("Danh sách người nhận Email rỗng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }


                    if (sttB == 0)
                    {
                        // Thêm mới lot vào database công đoạn bọc
                        result = DatabaseHelper.UpdateKhoiLuongConLai<TonKho, DL_CD_Boc>(tonKhoMoi, dL_CD_Boc, "DL_CD_Boc", tonKho_update);

                    }
                    else
                    {
                        MessageBox.Show("Thao tác thất bại. Liên hệ Mr.Linh để sửa dữ liệu nếu cần", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;

                        dL_CD_Boc.GhiChu = dL_CD_Boc.GhiChu + "- Đã sửa";
                        result = DatabaseHelper.Update_Mica(sttB, tonKhoMoi, dL_CD_Boc, tonKho_update);
                    }

                    ResetAllController();
                    if (result) MessageBox.Show("THAO TÁC THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            //may.SelectedIndex = -1;

            dgDsLot.Rows.Clear();
            maySX.SelectedIndex = -1;

            //lot.Text = "";
            maSP.Text = "";
            idTenSP.Value = 0;
            tenSP.Text = "";
            //klTruocBoc.Value = 0;
            klPhe.Value = 0;
            chieuDai.Value = 0;
            //klConLai.Value = 0;
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
            //may.SelectedIndex = -1;
            //maHT.Value = 0;
            //STTCD.SelectedIndex = -1;
            //sttBin.Value = 0;
            //soBin.Value = 0;
            //lot.Clear();
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
                SELECT 
	                t.ID, 
	                t.Lot, 
                    0 as ConLai,
	                dssp.ten as TenSP,
	                t.KhoiLuongConLai as KL
                FROM DL_CD_Ben b
                JOIN TonKho t ON b.TonKho_ID = t.ID
                JOIN DanhSachMaSP dssp ON t.MaSP_ID = dssp.ID
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

        private void dgDsLot_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgDsLot.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                var confirm = MessageBox.Show("Bạn có chắc muốn xóa dòng này?",
                                              "Xác nhận xóa",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    dgDsLot.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void cbTimLot_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ResetController_TimLOT();


            if (cbTimLot.SelectedItem == null || !(cbTimLot.SelectedItem is DataRowView)) return;

            DataRowView row = (DataRowView)cbTimLot.SelectedItem;

            string id = row["ID"].ToString();

            //DataRowView dong = (DataRowView)row;

            bool isDuplicate = dgDsLot.Rows.Cast<DataGridViewRow>()
                .Any(r => r.Cells["ID"].Value?.ToString() == id);

            if (!isDuplicate)
            {
                int index = dgDsLot.Rows.Add();
                DataGridViewRow newRow = dgDsLot.Rows[index];
                newRow.Cells["ID"].Value = row["ID"];
                newRow.Cells["lot"].Value = row["Lot"];
                newRow.Cells["conLai"].Value = row["ConLai"];
                newRow.Cells["ten"].Value = row["TenSP"];
                newRow.Cells["kl"].Value = row["KL"];
            }
            else
            {
                MessageBox.Show("Lô này đã được thêm vào danh sách.");
            }

            cbTimLot.SelectedIndex = -1;  // Bỏ chọn item
            cbTimLot.Text = string.Empty; // Cho chắc chắn
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
                    b.*, 
                    p.ID              AS ID_HienTai,
                    p.Lot             AS Lot_HienTai,
                    p.KhoiLuongDauVao AS KL_HienTai,
                    p.ChieuDai           ,
                    spp.Ten           AS TenSP_HienTai,
                    spp.Ma           AS Ma_HienTai,
	
                    c.ID              AS ID_HanNoi,
                    c.Lot             AS Lot_NVL,
                    c.KhoiLuongConLai,
                    sp.Ten            AS Ten_NVL
                FROM TonKho AS c
                JOIN TonKho AS p
                    ON p.ID = c.HanNoi
                LEFT JOIN DanhSachMaSP AS sp
                    ON sp.ID = c.MaSP_ID
                LEFT JOIN DanhSachMaSP AS spp
                    ON spp.ID = p.MaSP_ID
                LEFT JOIN DL_CD_Boc AS b
                    ON b.TonKho_ID = p.ID
                WHERE p.ID = @ID";


            DataTable data = DatabaseHelper.GetDL_CDBenByID(id_MaSP, query);

            // check data return
            if (data.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu với ID được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int totalCols = data.Columns.Count;

            // Giả sử dgDsLot đã có sẵn cột (ít nhất 6 cột vì bạn định đổ từ cột số 2)
            dgDsLot.Rows.Clear();

            // Duyệt từng dòng của DataTable
            foreach (DataRow row in data.Rows)
            {
                int index = dgDsLot.Rows.Add(); // thêm một dòng mới vào DataGridView
                DataGridViewRow dgRow = dgDsLot.Rows[index];

                // Gán dữ liệu 4 cột cuối vào DataGridView, bắt đầu từ cột thứ 2
                for (int i = 0; i < 4; i++)
                {
                    dgRow.Cells[i + 1].Value = row[totalCols - 4 + i];
                }
            }




            DataRow dataRow = data.Rows[0];

            ngay.Text = dataRow["ngay"].ToString();
            ca.Text = dataRow["ca"].ToString();
            maySX.Text = dataRow["soMay"].ToString();
            maSP.Text = dataRow["Ma_HienTai"].ToString();
            idTenSP.Value = Convert.ToDecimal(dataRow["MaSP_id"]);
            tenSP.Text = dataRow["TenSP_HienTai"].ToString();
            congDoan.Text = dataRow["TenCongDoan"].ToString();
            klPhe.Value = Convert.ToDecimal(dataRow["KhoiLuongPhe"]);
            chieuDai.Value = Convert.ToDecimal(dataRow["ChieuDai"]);
            nguoiLam.Text = dataRow["NguoiLam"].ToString();
            ghiChu.Text = dataRow["ghiChu"].ToString();
            tbKhoiLuongTruocBoc.Text = dataRow["KhoiLuongTruocBoc"].ToString();

            isProgrammaticChange = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetAllController();
        }

        private async void showReport_Click_1(object sender, EventArgs e)
        {
            string dateRP = dateReport.Value.Date.ToString("yyyy-MM");


            string query = @"
                SELECT
                    c.ID          AS ID_NVL,
                    c.Lot         AS Lot_NVL,
                    sp.Ten        AS Ten_NVL,
                    p.ID          AS ID_HienTai,
                    p.KhoiLuongDauVao AS KL_HienTai,
                    spp.Ten       AS TenSP_HienTai,
                    p.Lot         AS Lot_HienTai
                FROM TonKho AS c
                JOIN TonKho AS p
                    ON p.ID = c.HanNoi
                LEFT JOIN DanhSachMaSP AS sp
                    ON sp.ID = c.MaSP_ID
                LEFT JOIN DanhSachMaSP AS spp
                    ON spp.ID = p.MaSP_ID
                JOIN DL_CD_Boc AS boc
                    ON boc.tonkho_id = p.ID
                WHERE c.HanNoi <> 0 and  strftime('%Y-%m', boc.Ngay) = @Ngay
                ORDER BY p.ID DESC, c.ID;

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

        private void cbXuatExcel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgDsLot_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                string input = e.FormattedValue.ToString();

                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Cột này không được để trống!");
                    e.Cancel = true; // Giữ focus trong ô
                    return;
                }

                // Kiểm tra có phải số decimal không
                if (!decimal.TryParse(input, out decimal value))
                {
                    MessageBox.Show("Cột này chỉ được nhập số!");
                    e.Cancel = true;
                    return;
                }

                // Kiểm tra thêm điều kiện nghiệp vụ (ví dụ: không âm)
                if (value < 0)
                {
                    MessageBox.Show("Giá trị phải >= 0!");
                    e.Cancel = true;
                }
            }
        }
    }
}

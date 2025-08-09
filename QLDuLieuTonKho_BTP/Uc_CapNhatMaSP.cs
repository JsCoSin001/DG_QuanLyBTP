using QLDuLieuTonKho_BTP.Data;
using QLDuLieuTonKho_BTP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QLDuLieuTonKho_BTP.Helper;

namespace QLDuLieuTonKho_BTP
{
    public partial class Uc_CapNhatMaSP : UserControl, ICustomUserControl
    {
        public string URL;
        public string[] LOAISP = { "Bán Thành Phẩm", "Thành Phẩm", "Nguyên Liệu" };
        public string[] KIHIEU_LOAISP = { "BTP", "TP", "NVL" };
        public event Action<DataTable> OnDataReady;

        public Uc_CapNhatMaSP(string url)
        {
            InitializeComponent();

            URL = url;
            DatabaseHelper.SetDatabasePath(url);
            tbUserPassword.Text = Properties.Settings.Default.UserPass;
        }

        public Uc_CapNhatMaSP(){}

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxLoaiSP.Items.Add(LOAISP[0]);
            cbxLoaiSP.Items.Add(LOAISP[1]);
            cbxLoaiSP.Items.Add(LOAISP[2]);
            cbxLoaiSP.Items.Add("Toàn Bộ");
            cbxLoaiSP.SelectedIndex = 0;
        }

        private bool kiemTraPhanQuyen(string tx)
        {
            string password = Properties.Settings.Default.PassApp;
            if(tx == password)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnLuuSP_Click(object sender, EventArgs e)
        {

            if (!kiemTraPhanQuyen(tbUserPassword.Text.Trim())) return;

            string ma = tbMa.Text.Trim().ToUpper();
            string ten = tbTenX.Text.Trim().ToUpper();



            string typeProduct = DatabaseHelper.KtraMaSP(ma);            


            if (typeProduct == "" || ten == "")
            {
                MessageBox.Show("Dữ liệu không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string id = idMaSP.Value.ToString();
            Boolean result = false;

            if (id != "0")
            {
                string sql = "UPDATE DanhSachMaSP SET Ma = @Ma, Ten = @Ten, KieuSP = @KieuSP WHERE ID = @ID";

                var parameters = new Dictionary<string, object>
                    {
                        {"@Ma", ma},
                        {"@Ten", ten},
                        {"@KieuSP", typeProduct},
                        {"@ID", id} // giả sử id là int
                    };

                result = DatabaseHelper.UpdateATable(sql, parameters);
            }
            else
            {
                result = DatabaseHelper.InsertSP(ma, ten);
            }


            if (result)
            {
                tbTenX.Clear();
                tbMa.Clear();
                idMaSP.Value = 0; 

                MessageBox.Show("Thao tác thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string getSQL()
        {
            int userSelected = cbxLoaiSP.SelectedIndex;
            string typeOfTable;
            switch (userSelected)
            {
                case 0:
                    typeOfTable = KIHIEU_LOAISP[0];
                    break;
                case 1:
                    typeOfTable = KIHIEU_LOAISP[1];
                    break;
                case 2:
                    typeOfTable = KIHIEU_LOAISP[2];
                    break;
                default:
                    typeOfTable = "";
                    break;
            }
            string timeCheck = "2024-01-01";

            if (!cbTimer.Checked)
            {
                DateTime selectedDate = DateTime.Parse(timePicker.Text.Trim());
                timeCheck = selectedDate.ToString("yyyy-MM-dd");
            }
            string sql = Helper.GenerateSQL_GetAll(timeCheck);

            if (typeOfTable != "")
            {
                sql = Helper.GenerateSQL_GetData(typeOfTable, timeCheck);
            }
            return sql;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            string sql = getSQL();

            var table = DatabaseHelper.GetDataFromSQL(sql);
            OnDataReady?.Invoke(table);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timePicker.Enabled = !timePicker.Enabled;
        }
                
        private async void button1_Click(object sender, EventArgs e)
        {
            string sql = getSQL();

            var table = DatabaseHelper.GetDataFromSQL(sql);

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nào được tìm thấy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fileName = $"DanhSachMaSP";
            await ExcelHelper.ExportWithLoading(table, fileName);

        }
         
        private async void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (!kiemTraPhanQuyen(tbUserPassword.Text.Trim())) return;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn file Excel";
                openFileDialog.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls";
                openFileDialog.Multiselect = false;

                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog.FileName))
                {
                    string excelPath = openFileDialog.FileName;
                    string message = "Đang import dữ liệu từ Excel...";
                    var loadingControl = new Uc_LoadingForm(message);

                    // 1. Tạo form chứa UserControl
                    Form loadingForm = new Form
                    {
                        FormBorderStyle = FormBorderStyle.None,
                        StartPosition = FormStartPosition.CenterScreen,
                        Size = loadingControl.Size,
                        ControlBox = false,
                        TopMost = true,
                        ShowInTaskbar = false
                    };
                    loadingControl.Dock = DockStyle.Fill;
                    loadingForm.Controls.Add(loadingControl);

                    // 2. Hiển thị form loading
                    loadingForm.Show();

                    try
                    {
                        // 3. Thực hiện import trong Task
                        await Task.Run(() =>
                        {
                            var dbHelper = new ExcelHelper();
                            dbHelper.ImportExcelProductList(excelPath, URL, loadingControl); 
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi import dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // 4. Đóng form loading sau khi hoàn tất
                        if (loadingForm.InvokeRequired)
                            loadingForm.Invoke(new Action(() => loadingForm.Close()));
                        else
                            loadingForm.Close();

                        MessageBox.Show("Import dữ liệu hoàn tất!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else                
                    MessageBox.Show("Không có file Excel nào được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = idMaSP.Value.ToString();
            if (id == "0") return;

            string query = "SELECT id, ma, ten FROM DanhSachMaSP WHERE id = @search";
            List<ProductModel> dta = DatabaseHelper.GetProductNamesAndPartNumber(query, id);

            if (dta.Count == 0)
            {
                MessageBox.Show("Không tồn tại ID này.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProductModel product = dta[0];
            tbMa.Text = product.Ma;
            tbTenX.Text = product.Ten;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string userP = tbUserPassword.Text.Trim();
            Helper.UpdatePassApp(userP);
            Properties.Settings.Default.UserPass = userP;
            Properties.Settings.Default.Save();
            MessageBox.Show("Câp nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}

using ClosedXML.Excel;
using QLDuLieuTonKho_BTP;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ExcelHelper
{
    public static void ExportToExcel(DataTable table, string filePath)
    {
        try
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Ghi tiêu đề cột
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    worksheet.Cell(1, col + 1).Value = table.Columns[col].ColumnName;
                    worksheet.Cell(1, col + 1).Style.Font.Bold = true;
                }

                // Ghi dữ liệu
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        // Explicitly convert the object to XLCellValue
                        worksheet.Cell(row + 2, col + 1).Value = XLCellValue.FromObject(table.Rows[row][col]);
                    }
                }

                // Tự động điều chỉnh độ rộng cột
                worksheet.Columns().AdjustToContents();

                // Lưu file Excel
                workbook.SaveAs(filePath);
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi nếu cần
            throw new Exception("Lỗi khi xuất file Excel: " + ex.Message, ex);
        }
    }


    public void ImportExcelProductList(string excelFilePath, string databasePath, Uc_LoadingForm loadingForm = null)
    {
        try
        {
            if (!File.Exists(excelFilePath))
            {
                MessageBox.Show("Không tìm thấy file Excel.");
                return;
            }

            using (var workbook = new XLWorkbook(excelFilePath))
            {
                var worksheet = workbook.Worksheet(1); // Lấy sheet đầu tiên
                var rows = worksheet.RangeUsed().RowsUsed();

                using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
                {
                    connection.Open();


                    foreach (var row in rows) 
                    {
                        string maSP = row.Cell(1).GetString().Trim();
                        string tenSP = row.Cell(2).GetString().Trim();

                        if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP))
                            continue;

                        //string[] parts = maSP.Split('.');
                        //if (parts.Length != 2)
                        //    continue;

                        string tableName = Helper.LayKieuSP(maSP);
                        if (tableName == "") continue;

                        //string tableName = GetTableNameFromPrefix(prefix);
                        if (tableName == null)
                            continue;

                        InsertProduct(connection, tableName, maSP, tenSP);
                    }
                }

                //MessageBox.Show("Import dữ liệu thành công.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi import dữ liệu: " + ex.Message);
        }
    }

    private void InsertProduct(SQLiteConnection connection, string tableName, string ma, string ten)
    {
        string sql = $@"
            INSERT OR IGNORE INTO DanhSachMaSP (Ma, Ten, KieuSP, DateInsert)
            VALUES (@ma, @ten,@tableName, @dateInsert);
        ";

        using (var cmd = new SQLiteCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@ma", ma);
            cmd.Parameters.AddWithValue("@ten", ten);
            cmd.Parameters.AddWithValue("@tableName", tableName);
            cmd.Parameters.AddWithValue("@dateInsert", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.ExecuteNonQuery();
        }
    }

    public static async Task ExportWithLoading(DataTable table, string defaultFileName = "Export.xlsx")
    {
        using (SaveFileDialog sfd = new SaveFileDialog()
        {
            Filter = "Excel Workbook|*.xlsx",
            FileName = $"{defaultFileName.Replace(".xlsx", "")} - {DateTime.Now:yyyy-MM-dd HH_mm}.xlsx"
        })
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string mes = "Đang xuất Excel, Xin hãy chờ ...";
                var loadingControl = new Uc_LoadingForm(mes);

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

                loadingForm.Show();

                await Task.Run(() =>
                {
                    ExportToExcel(table, sfd.FileName);
                });

                if (loadingForm.InvokeRequired)
                {
                    loadingForm.Invoke(new Action(() => loadingForm.Close()));
                }
                else
                {
                    loadingForm.Close();
                }

                MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

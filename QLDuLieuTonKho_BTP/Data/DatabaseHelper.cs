using QLDuLieuTonKho_BTP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace QLDuLieuTonKho_BTP.Data
{

    public static class DatabaseHelper
    {
        private static string connStr;
        public static void SetDatabasePath(string path)
        {
            connStr = $"Data Source={path};Version=3;";
        }
 
        public static Boolean InsertSanPhamTonKhoDL<TTonKho, TDLCongDoan>(TTonKho tonKho, TDLCongDoan dlModel, string table)
        where TTonKho : class
        where TDLCongDoan : class
        {
            using (var connection = new SQLiteConnection(connStr))
            {
                connection.Open();
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        InsertModelToDatabase(tonKho, "TonKho", connection, tran);
                        int tonKho_ID = (int)(long)new SQLiteCommand("SELECT last_insert_rowid()", connection, tran).ExecuteScalar();

                        // Gán thuộc tính TonKho_ID bằng reflection hoặc dynamic
                        dynamic dynamicModel = dlModel;
                        dynamicModel.TonKho_ID = tonKho_ID;

                        InsertModelToDatabase(dynamicModel, table, connection, tran);

                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Lỗi: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

        }

        public static void InsertModelToDatabase<T>(T model, string tableName, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead)
                .Where(p => !Attribute.IsDefined(p, typeof(AutoIncrementAttribute))) // Bỏ qua AutoIncrement
                .ToList();

            string columns = string.Join(", ", properties.Select(p => p.Name));
            string parameters = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            using (var command = new SQLiteCommand(query, connection, transaction))
            {
                foreach (var prop in properties)
                {
                    object value = prop.GetValue(model) ?? DBNull.Value;
                    command.Parameters.AddWithValue($"@{prop.Name}", value);
                }

                command.ExecuteNonQuery();
            }
        }

        public static Boolean UpdateDL_CDBoc(int bocID, TonKho tonKho, DL_CD_Boc dl)
        {
            Boolean result = false;
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Lấy TonKho_ID từ bảng DL_CD_Boc
                        int tonKhoID = GetTonKhoIDFromID(conn, bocID,"boc");

                        // Cập nhật bảng TonKho
                        result = UpdateModelInDatabase(tonKho, "TonKho", "ID", tonKhoID, conn, tran);
                        dl.TonKho_ID = tonKhoID;

                        // Cập nhật bảng DL_CD_Boc
                        if (result) result = UpdateModelInDatabase(dl, "DL_CD_Boc", "ID", bocID, conn, tran);

                        tran.Commit();
                    }
                    catch(Exception ex) 
                    {
                        tran.Rollback();
                        MessageBox.Show("Lỗi: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result = false;
                    }
                }
            }
            return result;
        }

        public static Boolean UpdateDL_CDBen( int benID, TonKho tonKho, DL_CD_Ben ben)
        {
            Boolean result = true;
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Lấy TonKho_ID từ bảng DL_CD_Ben
                        int tonKhoID = GetTonKhoIDFromID(conn, benID);

                        // Cập nhật bảng TonKho
                        result = UpdateModelInDatabase(tonKho, "TonKho", "ID", tonKhoID, conn, tran);

                        ben.TonKho_ID = tonKhoID;

                        // Cập nhật bảng DL_CD_Ben
                        if(result) result = UpdateModelInDatabase(ben, "DL_CD_Ben", "ID", benID, conn, tran);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Lỗi: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result = false;
                    }
                }
            }
            return result;
        }

        private static Boolean UpdateModelInDatabase<T>(T model, string tableName, string keyColumn, object keyValue, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            try
            {
                var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.Name != keyColumn) // Bỏ qua cột khóa chính
                .ToList();

                string setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
                string query = $"UPDATE {tableName} SET {setClause} WHERE {keyColumn} = @KeyValue";

                using (var command = new SQLiteCommand(query, connection, transaction))
                {
                    foreach (var prop in properties)
                    {
                        object value = prop.GetValue(model) ?? DBNull.Value;
                        command.Parameters.AddWithValue($"@{prop.Name}", value);
                    }

                    command.Parameters.AddWithValue("@KeyValue", keyValue);
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }            
        }

        public static Boolean UpdateTonKho_SLConLaiThucTe(TonKho tk)
        {
            Boolean result = true;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                string query = @"
                UPDATE TonKho 
                SET KhoiLuongConLai = @KhoiLuongMoi 
                WHERE Lot = @Lot";

                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KhoiLuongMoi", tk.KhoiLuongConLai);
                    cmd.Parameters.AddWithValue("@Lot", tk.Lot);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool UpdateATable(string sql, Dictionary<string, object> parameters)
        {
            int affectedRows = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        foreach (var p in parameters) cmd.Parameters.AddWithValue(p.Key, p.Value);

                        conn.Open();
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }

            return affectedRows != 0;
        }

        private static int GetTonKhoIDFromID(SQLiteConnection connection, int id, string table = "Ben")
        {
            using (var command = new SQLiteCommand("SELECT TonKho_ID FROM DL_CD_"+ table +" WHERE ID = @ID", connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                var result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    throw new Exception("TonKho_ID not found for DL_CD_\"+ table +\".ID = " + id);

                return Convert.ToInt32(result);
            }
        }

        public static DataTable GetDataByDate(string ngay, string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ngay", ngay);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);
                        return resultTable;
                    }
                }
            }
        }

        public static DataTable GetTonKho(string lot, string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Lot", lot);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);
                        return resultTable;
                    }
                }
            }
        }

        public static DataTable GetDL_CDBenByID(int id, string query)
        {
            DataTable resultTable = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(resultTable);
                    }
                }
            }

            return resultTable;
        }

        public static List<ProductModel> GetProductNamesAndPartNumber(string query, string keyword)
        {
            List<ProductModel> results = new List<ProductModel>();

            //string connStr = $"Data Source={dbPath};Version=3;";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", keyword);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string ma = reader["ma"].ToString();
                                string ten = reader["ten"].ToString();
                                results.Add(new ProductModel(id, ma, ten));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQLite: " + ex.Message);
            }

            return results;
        }


        // Lấy danh sách mã sp
        public static DataTable GetDanhSachMaSP( string query)
        {

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);
                        return resultTable;
                    }
                }
            }
        }
                
        public static string PartNumber_IsRealy(string ma)
        {
            string query = $"SELECT ten FROM DanhSachMaSP WHERE Ma = @ma";

            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }

        public static Boolean InsertSP( string ma, string ten)
        {

            string typeProduct = KtraMaSP(ma);

            if (typeProduct == "") return false;

            string insertQuery = "INSERT INTO DanhSachMaSP"+
                         " (Ma, Ten, KieuSP ,DateInsert) VALUES (@ma, @ten, @typeProduct, strftime('%Y-%m-%d', 'now', 'localtime'));";
            try

            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", ma);
                        cmd.Parameters.AddWithValue("@ten", ten);
                        cmd.Parameters.AddWithValue("@typeProduct", typeProduct);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Lỗi khi ghi vào database:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static string KtraMaSP(string ma)
        {
            int dotIndex = ma.IndexOf('.');

            string typeProduct = dotIndex >= 0 ? ma.Substring(0, dotIndex) : ma;
            typeProduct = typeProduct.ToUpper();

            if (typeProduct != "NVL" && typeProduct != "BTP" && typeProduct != "TP")
            {
                MessageBox.Show("Sai cấu trúc Mã Sản Phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";

            }
            return typeProduct;
        }

        public static void ShowMessageForSeconds(string message, Label lblMessage, int timeInterval = 5000)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = timeInterval;

            timer.Tick += (s, e) =>
            {
                lblMessage.Visible = false;
                timer.Stop();
                timer.Dispose();
            };

            timer.Start();
        }




    }


}

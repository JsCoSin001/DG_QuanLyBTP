using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDuLieuTonKho_BTP.Validate
{
    internal class URL
    {
        public static bool CheckFile(string filePath, Form parent)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Không tìm thấy file cấu hình", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                parent.Close();
                return false;
            }
            return true;
        }



    }
}

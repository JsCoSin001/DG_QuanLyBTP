using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuLieuTonKho_BTP.Models
{
    public class DanhSachMaSP
    {
        public int id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string KieuSP { get; set; }
        public string DateInsert { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    }
}

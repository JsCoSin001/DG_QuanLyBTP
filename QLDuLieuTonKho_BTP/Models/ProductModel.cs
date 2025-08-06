using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuLieuTonKho_BTP.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }

        public ProductModel() { }

        public ProductModel(int id, string ma, string ten)
        {
            ID = id;
            Ma = ma;
            Ten = ten;
        }

        public override string ToString()
        {
            return Ten; // để ComboBox hiển thị tên
        }
    }
}

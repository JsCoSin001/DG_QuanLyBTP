using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuLieuTonKho_BTP.Models
{
    public class TonKho
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string Lot { get; set; }
        public int MaSP_ID { get; set; }
        public decimal KhoiLuongDauVao { get; set; }
        public decimal KhoiLuongConLai { get; set; }
        public int HanNoi { get; set; }
        public decimal ChieuDai { get; set; }
    }
}

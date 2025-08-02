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
        public string Lot { get; set; }
        public int MaSP_ID { get; set; }
        public double KhoiLuongDauVao { get; set; }
        public double KhoiLuongConLai { get; set; }
        public double HanNoi { get; set; }
        public double ChieuDai { get; set; }
    }
}

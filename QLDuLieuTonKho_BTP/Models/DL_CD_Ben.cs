using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuLieuTonKho_BTP.Models
{
    public class DL_CD_Ben
    {
        [Required(ErrorMessage = "Ngày không được để trống")]
        public string Ngay { get; set; }

        [Required(ErrorMessage = "Ca không được để trống")]
        public string Ca { get; set; }

        [Required(ErrorMessage = "TonKho_ID không được để trống")]
        public int TonKho_ID { get; set; }

        [Required(ErrorMessage = "Người làm không được để trống")]
        public string NguoiLam { get; set; }

        [Required(ErrorMessage = "Số máy không được để trống")]
        public string SoMay { get; set; }
        public string GhiChu { get; set; }
        public string DateInsert { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

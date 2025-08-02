using System;
using System.ComponentModel.DataAnnotations;
[AttributeUsage(AttributeTargets.Property)]

public class AutoIncrementAttribute : Attribute { }
public class DL_CD_Boc
{
    [AutoIncrement]
    public int ID { get; set; }

    [Required(ErrorMessage = "Ngày không được để trống")]
    public string Ngay { get; set; }

    [Required(ErrorMessage = "Ca không được để trống")]
    public string Ca { get; set; }
    public double KhoiLuongTruocBoc { get; set; }
    public double KhoiLuongPhe { get; set; } = 0;

    [Required(ErrorMessage = "Người làm không được để trống")]
    public string NguoiLam { get; set; }

    [Required(ErrorMessage = "Số máy không được để trống")]
    public string SoMay { get; set; }

    public string GhiChu { get; set; }
    public string TenCongDoan { get; set; }

    public string DateInsert { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    public int MaSP_ID { get; set; } // Foreign key to DanhSachMaSP

    public int? CD_Ben_ID { get; set; } // Foreign key to DL_CD_Ben (nullable)

    public int TonKho_ID { get; set; }
}

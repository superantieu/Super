using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[Table("Hang")]
public partial class Hang
{
   
    [Key]
    public int MaHang { get; set; }

    [StringLength(150)]
    public string? TenHang { get; set; }

    [StringLength(50)]
    public string? DonGiaHang { get; set; }

    [StringLength(50)]
    public string? MaNhanHieu { get; set; }

    [Column(TypeName = "ntext")]
    public string? MoTa { get; set; }

    public int? TonKho { get; set; }

    public string? HinhAnh { get; set; }

    [StringLength(200)]
    public string? Filter { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(50)]
    public string? Url { get; set; }

    [StringLength(50)]
    public string? Src { get; set; }

    [Column(TypeName = "date")]
    public DateTime? NgayNhap { get; set; }

    [InverseProperty("MaHangNavigation")]
    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    [ForeignKey("MaNhanHieu")]
    [InverseProperty("Hangs")]
    public virtual NhanHieu? MaNhanHieuNavigation { get; set; }

    [ForeignKey("Url")]
    [InverseProperty("Hangs")]
    public virtual Balance? UrlNavigation { get; set; }
}

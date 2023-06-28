using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[PrimaryKey("MaHd", "MaHang")]
[Table("ChiTietHoaDon")]
public partial class ChiTietHoaDon
{
    [Key]
    [Column("MaHD")]
    public int MaHd { get; set; }

    [Key]
    public int MaHang { get; set; }

    public int? SoLuongMua { get; set; }

    [StringLength(50)]
    public string? DonGiaBan { get; set; }

    [StringLength(50)]
    public string? ThanhTien { get; set; }

    [StringLength(50)]
    public string? TongTien { get; set; }

    public string? Filter { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("MaHang")]
    [InverseProperty("ChiTietHoaDons")]
    public virtual Hang MaHangNavigation { get; set; } = null!;

    [ForeignKey("MaHd")]
    [InverseProperty("ChiTietHoaDons")]
    public virtual HoaDon MaHdNavigation { get; set; } = null!;
}

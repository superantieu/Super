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

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? DonGiaBan { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? ThanhTien { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TongTien { get; set; }

    public string? Filter { get; set; }

    public bool? IsActive { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }
}

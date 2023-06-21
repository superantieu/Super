using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[Table("KhachHang")]
public partial class KhachHang
{
    [Key]
    [Column("MaKH")]
    public int MaKh { get; set; }

    [StringLength(150)]
    public string? TenKhachHang { get; set; }

    [StringLength(150)]
    public string? DiaChi { get; set; }

    [StringLength(50)]
    public string? DienThoai { get; set; }

    [InverseProperty("MaKhNavigation")]
    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}

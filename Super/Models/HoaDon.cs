using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[Table("HoaDon")]
public partial class HoaDon
{
    [Key]
    [Column("MaHD")]
    public int MaHd { get; set; }

    [Column(TypeName = "date")]
    public DateTime? NgayLap { get; set; }

    [Column("MaKH")]
    public int? MaKh { get; set; }

    [InverseProperty("MaHdNavigation")]
    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    [ForeignKey("MaKh")]
    [InverseProperty("HoaDons")]
    public virtual KhachHang? MaKhNavigation { get; set; }
}

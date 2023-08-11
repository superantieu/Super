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

    public string? Filter { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TongTien { get; set; }
}

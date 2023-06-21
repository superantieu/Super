using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[Table("NhanHieu")]
public partial class NhanHieu
{
    [Key]
    [StringLength(50)]
    public string MaNhanHieu { get; set; } = null!;

    [StringLength(50)]
    public string? TenNhanHieu { get; set; }

    [StringLength(50)]
    public string? MaCungCap { get; set; }

    [InverseProperty("MaNhanHieuNavigation")]
    public virtual ICollection<Hang> Hangs { get; set; } = new List<Hang>();

    [ForeignKey("MaCungCap")]
    [InverseProperty("NhanHieus")]
    public virtual NhaCungCap? MaCungCapNavigation { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[Table("NhaCungCap")]
public partial class NhaCungCap
{
    [Key]
    [StringLength(50)]
    public string MaCungCap { get; set; } = null!;

    [StringLength(50)]
    public string? TenNhaCungCap { get; set; }

    [StringLength(50)]
    public string? DienThoai { get; set; }

    [StringLength(150)]
    public string? DiaChi { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    public string? Filter { get; set; }

    public bool? IsActive { get; set; }

    [InverseProperty("MaCungCapNavigation")]
    public virtual ICollection<NhanHieu> NhanHieus { get; set; } = new List<NhanHieu>();
}

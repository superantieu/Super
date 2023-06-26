using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [Column("RoleID")]
    public int? RoleId { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? Password { get; set; }

    [Column("MaKH")]
    public int? MaKh { get; set; }

    [ForeignKey("MaKh")]
    [InverseProperty("Users")]
    public virtual KhachHang? MaKhNavigation { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[Table("KMDB")]
public partial class Kmdb
{
    [Key]
    [StringLength(50)]
    public string Url { get; set; } = null!;

    [StringLength(50)]
    public string? KhuyenMai { get; set; }

    [Column(TypeName = "ntext")]
    public string? ChiTietKhuyenMai { get; set; }
}

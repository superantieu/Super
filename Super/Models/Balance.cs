using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

[Table("Balance")]
public partial class Balance
{
    [Key]
    [StringLength(50)]
    public string Url { get; set; } = null!;

    [Column("KM")]
    [StringLength(50)]
    public string? Km { get; set; }
    public bool? IsActive { get; set; }
    [StringLength(200)]
    public string? Filter { get; set; }

    [Column(TypeName = "ntext")]
    public string? Details { get; set; }

    [InverseProperty("UrlNavigation")]
    public virtual ICollection<Hang> Hangs { get; set; } = new List<Hang>();
}

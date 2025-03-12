using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("rapportinspection", Schema = "upways")]
public partial class Rapportinspection
{
    [Key]
    [Column("idinspection")]
    public int Idinspection { get; set; }

    [Column("typeinspection")]
    [StringLength(200)]
    public string? Typeinspection { get; set; }

    [Column("soustypeinspection")]
    [StringLength(200)]
    public string? Soustypeinspection { get; set; }

    [Column("pointdinspection")]
    [StringLength(200)]
    public string? Pointdinspection { get; set; }

    [InverseProperty("IdinspectionNavigation")]
    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();
}

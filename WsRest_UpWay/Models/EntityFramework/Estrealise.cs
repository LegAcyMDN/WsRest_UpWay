using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idvelo", "Idinspection", "Idreparation")]
[Table("estrealise", Schema = "upways")]
[Index("Idinspection", Name = "idx_estrealise_idinspection")]
[Index("Idreparation", Name = "idx_estrealise_idreparation")]
[Index("Idvelo", Name = "idx_estrealise_idvelo")]
public partial class Estrealise
{
    [Key]
    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Key]
    [Column("idinspection")]
    public int Idinspection { get; set; }

    [Key]
    [Column("idreparation")]
    public int Idreparation { get; set; }

    [Column("dateinspection")]
    [StringLength(10)]
    public string? Dateinspection { get; set; }

    [Column("commentaireinspection")]
    [StringLength(4096)]
    public string? Commentaireinspection { get; set; }

    [Column("historiqueinspection")]
    [StringLength(100)]
    public string? Historiqueinspection { get; set; }

    [ForeignKey("Idinspection")]
    [InverseProperty("Estrealises")]
    public virtual Rapportinspection IdinspectionNavigation { get; set; } = null!;

    [ForeignKey("Idreparation")]
    [InverseProperty("Estrealises")]
    public virtual Reparationvelo IdreparationNavigation { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Estrealises")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

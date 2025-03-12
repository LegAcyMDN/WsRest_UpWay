using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("moteur", Schema = "upways")]
[Index("Idmarque", Name = "idx_moteur_idmarque")]
public partial class Moteur
{
    [Key]
    [Column("idmoteur")]
    public int Idmoteur { get; set; }

    [Column("idmarque")]
    public int Idmarque { get; set; }

    [Column("modelemoteur")]
    [StringLength(50)]
    public string? Modelemoteur { get; set; }

    [Column("couplemoteur")]
    [StringLength(10)]
    public string? Couplemoteur { get; set; }

    [Column("vitessemaximal")]
    [StringLength(10)]
    public string? Vitessemaximal { get; set; }

    [ForeignKey("Idmarque")]
    [InverseProperty("Moteurs")]
    public virtual Marque IdmarqueNavigation { get; set; } = null!;

    [InverseProperty("IdmoteurNavigation")]
    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty("IdmoteurNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}

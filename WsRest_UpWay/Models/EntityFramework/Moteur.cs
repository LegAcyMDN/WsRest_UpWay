using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_emoteur_mot", Schema = "upways")]
[Index("Idmarque", Name = "idx_moteur_idmarque")]
public partial class Moteur
{
    [Key]
    [Column("mot_id")]
    public int Idmoteur { get; set; }

    [Column("mar_id")]
    public int Idmarque { get; set; }

    [Column("mot_modele")]
    [StringLength(50)]
    public string? Modelemoteur { get; set; }

    [Column("mot_couple")]
    [StringLength(10)]
    public string? Couplemoteur { get; set; }

    [Column("mot_vitessemax")]
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

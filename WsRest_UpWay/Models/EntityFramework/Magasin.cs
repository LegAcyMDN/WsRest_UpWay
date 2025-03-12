using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_a_magasin_mag", Schema = "upways")]
public partial class Magasin
{
    [Key]
    [Column("mag_id")]
    public int Idmagasin { get; set; }

    [Column("mag_nom")]
    [StringLength(50)]
    public string? Nommagasin { get; set; }

    [Column("mag_horaire")]
    [StringLength(200)]
    public string? Horairemagasin { get; set; }

    [Column("mag_rue")]
    [StringLength(100)]
    public string? Ruemagasin { get; set; }

    [Column("mag_cp", TypeName = "char(5)")]
    [StringLength(5)]
    public string? Cpmagasin { get; set; }

    [Column("mag_ville")]
    [StringLength(50)]
    public string? Villemagasin { get; set; }

    [InverseProperty("IdmagasinNavigation")]
    public virtual ICollection<Retraitmagasin> Retraitmagasins { get; set; } = new List<Retraitmagasin>();

    [InverseProperty("IdmagasinNavigation")]
    public virtual ICollection<Testvelo> Testvelos { get; set; } = new List<Testvelo>();

    [ForeignKey("Idmagasin")]
    [InverseProperty("Idmagasins")]
    public virtual ICollection<Velo> Idvelos { get; set; } = new List<Velo>();
}

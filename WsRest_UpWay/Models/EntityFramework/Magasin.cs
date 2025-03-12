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
    public int MagasinId { get; set; }

    [Column("mag_nom")]
    [StringLength(50)]
    public string? NomMagasin { get; set; }

    [Column("mag_horaire")]
    [StringLength(200)]
    public string? HoraireMagasin { get; set; }

    [Column("mag_rue")]
    [StringLength(100)]
    public string? RueMagasin { get; set; }

    [Column("mag_cp", TypeName = "char(5)")]
    [StringLength(5)]
    public string? CPMagasin { get; set; }

    [Column("mag_ville")]
    [StringLength(50)]
    public string? VilleMagasin { get; set; }

    [InverseProperty("IdmagasinNavigation")]
    public virtual ICollection<Retraitmagasin> Retraitmagasins { get; set; } = new List<Retraitmagasin>();

    [InverseProperty("IdmagasinNavigation")]
    public virtual ICollection<Testvelo> Testvelos { get; set; } = new List<Testvelo>();

    [ForeignKey("Idmagasin")]
    [InverseProperty("Idmagasins")]
    public virtual ICollection<Velo> Idvelos { get; set; } = new List<Velo>();
}

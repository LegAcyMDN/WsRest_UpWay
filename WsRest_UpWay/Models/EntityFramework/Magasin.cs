using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_a_magasin_mag", Schema = "upways")]
public partial class Magasin
{
    public Magasin()
    {
        ListeTestVelos = new HashSet<Testvelo>();
        ListeRetraitMagasins = new HashSet<Retraitmagasin>();
        ListeVelos = new HashSet<Velo>();
    }

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
    [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Le code postal doit contenir 5 chiffres")]
    public string? CPMagasin { get; set; }

    [Column("mag_ville")]
    [StringLength(50)]
    public string? VilleMagasin { get; set; }

    [InverseProperty(nameof(Retraitmagasin.RetraitMagasinMagasin))]
    public virtual ICollection<Retraitmagasin> ListeRetraitMagasins { get; set; } = new List<Retraitmagasin>();

    [InverseProperty(nameof(Testvelo.TestVeloMagasin))]
    public virtual ICollection<Testvelo> ListeTestVelos { get; set; } = new List<Testvelo>();

    [ForeignKey(nameof(MagasinId))]
    [InverseProperty(nameof(Velo.ListeMagasins))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

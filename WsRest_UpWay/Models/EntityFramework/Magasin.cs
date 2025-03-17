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
        ListeTestVelos = new HashSet<TestVelo>();
        ListeRetraitMagasins = new HashSet<RetraitMagasin>();
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
    public string? CPMagasin { get; set; }

    [Column("mag_ville")]
    [StringLength(50)]
    public string? VilleMagasin { get; set; }

    [InverseProperty(nameof(RetraitMagasin.RetraitMagasinMagasin))]
    public virtual ICollection<RetraitMagasin> ListeRetraitMagasins { get; set; } = new List<RetraitMagasin>();

    [InverseProperty(nameof(TestVelo.TestVeloMagasin))]
    public virtual ICollection<TestVelo> ListeTestVelos { get; set; } = new List<TestVelo>();

    [ForeignKey(nameof(MagasinId))]
    [InverseProperty(nameof(Velo.ListeMagasins))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

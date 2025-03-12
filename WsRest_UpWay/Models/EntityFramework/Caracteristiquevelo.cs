using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_caracteristiquevelo_cav", Schema = "upways")]
public partial class Caracteristiquevelo
{
    [Key]
    [Column("cav_id")]
    public int CaracteristiqueVeloId { get; set; }

    [Column("cav_poids")]
    [Precision(5, 2)]
    public decimal Poids { get; set; }

    [Column("cav_tubeSelle")]
    public int TubeSelle { get; set; }

    [Column("cav_typeSuspension")]
    [StringLength(20)]
    public string? TypeSuspension { get; set; }

    [Column("cav_couleur")]
    [StringLength(10)]
    public string? Couleur { get; set; }

    [Column("cav_typeCargo")]
    [StringLength(20)]
    public string? TypeCargo { get; set; }

    [Column("cav_etatBatterie")]
    [StringLength(10)]
    public string? EtatBatterie { get; set; }

    [Column("cav_nombreCycle")]
    public int? NombreCycle { get; set; }

    [Column("cav_materiau")]
    [StringLength(20)]
    public string? Materiau { get; set; }

    [Column("cav_fourche")]
    [StringLength(50)]
    public string? Fourche { get; set; }

    [Column("cav_debattement")]
    public int? Debattement { get; set; }

    [Column("cav_amortisseur")]
    [StringLength(50)]
    public string? Amortisseur { get; set; }

    [Column("cav_debattementAmortisseur")]
    public int? DebattementAmortisseur { get; set; }

    [Column("cav_modelTransmission")]
    [StringLength(50)]
    public string? ModelTransmission { get; set; }

    [Column("cav_nombreVitesse")]
    public int? NombreVitesse { get; set; }

    [Column("cav_freins")]
    [StringLength(30)]
    public string? Freins { get; set; }

    [Column("cav_taillesRoues")]
    public int? TaillesRoues { get; set; }

    [Column("cav_pneus")]
    [StringLength(100)]
    public string? Pneus { get; set; }

    [Column("cav_selleTelescopique")]
    public bool? SelleTelescopique { get; set; }

    [InverseProperty("IdcaracteristiqueveloNavigation")]
    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty("IdcaracteristiqueveloNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}

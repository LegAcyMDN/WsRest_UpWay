using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_velo_vel", Schema = "upways")]
[Index("Idcaracteristiquevelo", Name = "idx_velo_idcaracteristiquevelo")]
[Index("Idcategorie", Name = "idx_velo_idcategorie")]
[Index("Idmarque", Name = "idx_velo_idmarque")]
[Index("Idmoteur", Name = "idx_velo_idmoteur")]
public partial class Velo
{
    [Key]
    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("mar_id")]
    public int? MarqueId { get; set; }

    [Column("cat_id")]
    public int CategorieId { get; set; }

    [Column("mot_id")]
    public int? MoteurId { get; set; }

    [Column("car_id")]
    public int? CaracteristiqueVeloId { get; set; }

    [Column("vel_nom")]
    [StringLength(200)]
    public string? NomVelo { get; set; }

    [Column("vel_annee")]
    [Precision(4, 0)]
    public decimal? AnneeVelo { get; set; }

    [Column("vel_taillemin")]
    [StringLength(15)]
    public string? TailleMin { get; set; }

    [Column("vel_taillemax")]
    [StringLength(15)]
    public string? TailleMax { get; set; }

    [Column("vel_kms")]
    [StringLength(15)]
    public string? NombreKms { get; set; }

    [Column("vel_prixremise")]
    [Precision(5, 0)]
    public decimal? PrixRemise { get; set; }

    [Column("vel_prixneuf")]
    [Precision(5, 0)]
    public decimal? PrixNeuf { get; set; }

    [Column("vel_pourcentagereduction")]
    [Precision(3, 0)]
    public decimal? PourcentageReduction { get; set; }

    [Column("vel_descriptif")]
    [StringLength(5000)]
    public string? DescriptifVelo { get; set; }

    [Column("vel_quantite")]
    [Precision(3, 0)]
    public decimal? QuantiteVelo { get; set; }

    [Column("vel_positionmoteur")]
    [StringLength(20)]
    public string? PositionMoteur { get; set; }

    [Column("vel_capacitebatterie")]
    [StringLength(10)]
    public string? CapaciteBatterie { get; set; }

    [InverseProperty("IdveloNavigation")]
    public virtual ICollection<Alertevelo> Alertevelos { get; set; } = new List<Alertevelo>();

    [InverseProperty("IdveloNavigation")]
    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();

    [ForeignKey("Idcaracteristiquevelo")]
    [InverseProperty("Velos")]
    public virtual Caracteristiquevelo? IdcaracteristiqueveloNavigation { get; set; }

    [ForeignKey("Idcategorie")]
    [InverseProperty("Velos")]
    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    [ForeignKey("Idmarque")]
    [InverseProperty("Velos")]
    public virtual Marque? IdmarqueNavigation { get; set; }

    [ForeignKey("Idmoteur")]
    [InverseProperty("Velos")]
    public virtual Moteur? IdmoteurNavigation { get; set; }

    [InverseProperty("IdveloNavigation")]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();

    [InverseProperty("IdveloNavigation")]
    public virtual ICollection<Mentionvelo> Mentionvelos { get; set; } = new List<Mentionvelo>();

    [InverseProperty("IdveloNavigation")]
    public virtual ICollection<Photovelo> Photovelos { get; set; } = new List<Photovelo>();

    [InverseProperty("IdveloNavigation")]
    public virtual ICollection<Testvelo> Testvelos { get; set; } = new List<Testvelo>();

    [InverseProperty("IdveloNavigation")]
    public virtual ICollection<Utilite> Utilites { get; set; } = new List<Utilite>();

    [ForeignKey("Idvelo")]
    [InverseProperty("Idvelos")]
    public virtual ICollection<Accessoire> Idaccessoires { get; set; } = new List<Accessoire>();

    [ForeignKey("Idvelo")]
    [InverseProperty("Idvelos")]
    public virtual ICollection<Caracteristique> Idcaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey("Idvelo")]
    [InverseProperty("Idvelos")]
    public virtual ICollection<Magasin> Idmagasins { get; set; } = new List<Magasin>();
}

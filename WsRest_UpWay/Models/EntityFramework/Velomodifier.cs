using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_velomodofier_vlm", Schema = "upways")]
[Index(nameof(CaracteristiqueVeloId), Name = "idx_velomodifier_idcaracteristiquevelomodifier")]
[Index(nameof(CategorieId), Name = "idx_velomodifier_idcategorie")]
[Index(nameof(MarqueId), Name = "idx_velomodifier_idmarque")]
[Index(nameof(MoteurId), Name = "idx_velomodifier_idmoteur")]
public partial class Velomodifier
{
    [Key]
    [Column("vlm_id")]
    public int VelomId { get; set; }

    [Column("vlm_idm")]
    public int VeloModifId { get; set; }

    [Column("mar_id")]
    public int? MarqueId { get; set; }

    [Column("cat_id")]
    public int CategorieId { get; set; }

    [Column("mot_id")]
    public int? MoteurId { get; set; }

    [Column("car_id")]
    public int? CaracteristiqueVeloId { get; set; }

    [Column("vlm_nom")]
    [StringLength(200)]
    public string? NomVelo { get; set; }

    [Column("vlm_annee")]
    [Precision(2, 0)]
    public int? AnneeVelo { get; set; }

    [Column("vlm_taillemin")]
    [StringLength(15)]
    public string? TailleMin { get; set; }

    [Column("vlm_taillemax")]
    [StringLength(15)]
    public string? TailleMax { get; set; }

    [Column("vlm_kms")]
    [StringLength(15)]
    public string? NombreKms { get; set; }

    [Column("vlm_prixremise", TypeName = "numeric(5, 2")]
    [Precision(5, 2)]
    public decimal? PrixRemise { get; set; }

    [Column("vlm_prixneuf", TypeName = "numeric(5, 2")]
    [Precision(5, 2)]
    public decimal? PrixNeuf { get; set; }

    [Column("vlm_pourcentagereduction", TypeName = "numeric(3, 0)")]
    [Precision(3, 0)]
    public decimal? PourcentageReduction { get; set; }

    [Column("vlm_descriptif", TypeName = "text")]
    [StringLength(4096)]
    public string? DescriptifVelo { get; set; }

    [Column("vlm_quantite")]
    [Precision(3, 0)]
    public decimal? QuantiteVelo { get; set; }

    [Column("vlm_positionmoteur")]
    [StringLength(20)]
    public string? PositionMoteur { get; set; }

    [Column("vlm_capacitebatterie")]
    [StringLength(10)]
    public string? CapaciteBatterie { get; set; }

    [Column("vlm_ancienprix", TypeName = "numeric(5, 2)")]
    [Precision(5, 2)]
    public decimal? AncienPrix { get; set; }

    [Column("modifier", TypeName = "date")]
    public DateTime? Modifier { get; set; }

    [ForeignKey(nameof(CaracteristiqueVeloId))]
    [InverseProperty(nameof(Caracteristiquevelo.ListeVeloModifiers))]
    public virtual Caracteristiquevelo? VeloModifCaracteristiqueVelo { get; set; }

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty(nameof(Categorie.ListeVeloModifiers))]
    public virtual Categorie VeloModifierCategorie { get; set; } = null!;

    [ForeignKey(nameof(VelomId))]
    [InverseProperty(nameof(Marque.ListeVeloModifiers))]
    public virtual Marque? VeloModifierMarque { get; set; }

    [ForeignKey(nameof(MoteurId))]
    [InverseProperty(nameof(Moteur.ListeVeloModifiers))]
    public virtual Moteur? VeloModifierMoteur { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_velomodofier_vlm", Schema = "upways")]
[Index(nameof(CaracteristiqueVeloId), Name = "ix_t_e_velomodifier_vlm_caracteristiqueveloid")]
[Index(nameof(CategorieId), Name = "ix_t_e_velomodifier_vlm_categorieid")]
[Index(nameof(MarqueId), Name = "ix_t_e_velomodifier_vlm_marqueid")]
[Index(nameof(MoteurId), Name = "ix_t_e_velomodifier_vlm_moteurid")]
public class VeloModifier : ISizedEntity
{
    [Key] [Column("vlm_id")] public int VelomId { get; set; }

    [Column("vlm_idm")] public int VeloModifId { get; set; }

    [Column("mar_id")] public int? MarqueId { get; set; }

    [Column("cat_id")] public int CategorieId { get; set; }

    [Column("mot_id")] public int? MoteurId { get; set; }

    [Column("car_id")] public int? CaracteristiqueVeloId { get; set; }

    [Column("vlm_nom")]
    [StringLength(200)]
    public string? NomVelo { get; set; }

    [Column("vlm_annee")]
    [Precision(4, 0)]
    public decimal? AnneeVelo { get; set; }

    [Column("vlm_taillemin")]
    [StringLength(15)]
    public string? TailleMin { get; set; }

    [Column("vlm_taillemax")]
    [StringLength(15)]
    public string? TailleMax { get; set; }

    [Column("vlm_kms")] [StringLength(15)] public string? NombreKms { get; set; }

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
    [InverseProperty(nameof(CaracteristiqueVelo.ListeVeloModifiers))]
    public virtual CaracteristiqueVelo? VeloModifCaracteristiqueVelo { get; set; }

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty(nameof(Categorie.ListeVeloModifiers))]
    public virtual Categorie VeloModifierCategorie { get; set; } = null!;

    [ForeignKey(nameof(VelomId))]
    [InverseProperty(nameof(Marque.ListeVeloModifiers))]
    public virtual Marque? VeloModifierMarque { get; set; }

    [ForeignKey(nameof(MoteurId))]
    [InverseProperty(nameof(Moteur.ListeVeloModifiers))]
    public virtual Moteur? VeloModifierMoteur { get; set; }

    public long GetSize()
    {
        return sizeof(int) * 6 + sizeof(decimal) * 6 + NomVelo?.Length ?? 0 +
            TailleMin?.Length ?? 0 + TailleMax?.Length ?? 0 +
            NombreKms?.Length ?? 0 + DescriptifVelo?.Length ?? 0 +
            PositionMoteur?.Length ?? 0 + CapaciteBatterie?.Length ?? 0;
    }
}
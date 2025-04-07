using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_velo_vel", Schema = "upways")]
[Index(nameof(CaracteristiqueVeloId), Name = "ix_t_e_velo_vel_caracteristiqueveloid")]
[Index(nameof(CategorieId), Name = "ix_t_e_velo_vel_categorieid")]
[Index(nameof(MarqueId), Name = "ix_t_e_velo_vel_marqueid")]
[Index(nameof(MoteurId), Name = "ix_t_e_velo_vel_moteurid")]
public class Velo : ISizedEntity
{
    public Velo()
    {
        ListeAlerteVelos = new HashSet<AlerteVelo>();
        ListeEstRealises = new HashSet<EstRealise>();
        ListeLignePaniers = new HashSet<LignePanier>();
        ListeMentionVelos = new HashSet<MentionVelo>();
        ListePhotoVelos = new HashSet<PhotoVelo>();
        ListeTestVelos = new HashSet<TestVelo>();
        ListeMagasins = new HashSet<Magasin>();
        ListeCaracteristiques = new HashSet<Caracteristique>();
        ListeAccessoires = new HashSet<Accessoire>();
        ListeUtilites = new HashSet<Utilite>();
    }

    [Key] [Column("vel_id")] public int VeloId { get; set; }

    [Column("mar_id")] public int? MarqueId { get; set; }

    [Column("cat_id")] public int CategorieId { get; set; }

    [Column("mot_id")] public int? MoteurId { get; set; }

    [Column("car_id")] public int? CaracteristiqueVeloId { get; set; }

    [Column("vel_nom")]
    [StringLength(200)]
    public string? NomVelo { get; set; }

    [Column("vel_annee")]
    [Precision(4, 0)]
    public int? AnneeVelo { get; set; }

    [Column("vel_taillemin", TypeName = "char(15)")]
    [StringLength(15)]
    public string? TailleMin { get; set; }

    [Column("vel_taillemax", TypeName = "char(15)")]
    [StringLength(15)]
    public string? TailleMax { get; set; }

    [Column("vel_kms", TypeName = "char(15)")]
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

    [Column("vel_descriptif", TypeName = "text")]
    [StringLength(5000)]
    public string? DescriptifVelo { get; set; }

    [Column("vel_quantite")]
    [Precision(3, 0)]
    public int? QuantiteVelo { get; set; }

    [Column("vel_positionmoteur")]
    [StringLength(20)]
    public string? PositionMoteur { get; set; }

    [Column("vel_capacitebatterie")]
    [StringLength(10)]
    public string? CapaciteBatterie { get; set; }

    [ForeignKey(nameof(CaracteristiqueVeloId))]
    [InverseProperty(nameof(CaracteristiqueVelo.ListeVelos))]
    public virtual CaracteristiqueVelo? VeloCaracteristiqueVelo { get; set; }

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty(nameof(Categorie.ListeVelos))]
    public virtual Categorie VeloCategorie { get; set; } = null!;

    [ForeignKey(nameof(MarqueId))]
    [InverseProperty(nameof(Marque.ListeVelos))]
    public virtual Marque? VeloMarque { get; set; }

    [ForeignKey(nameof(MoteurId))]
    [InverseProperty(nameof(Moteur.ListeVelos))]
    public virtual Moteur? VeloMoteur { get; set; }

    [InverseProperty(nameof(AlerteVelo.AlerteVeloVelo))]
    public virtual ICollection<AlerteVelo> ListeAlerteVelos { get; set; } = new List<AlerteVelo>();

    [InverseProperty(nameof(EstRealise.EstRealiseVelo))]
    public virtual ICollection<EstRealise> ListeEstRealises { get; set; } = new List<EstRealise>();

    [InverseProperty(nameof(LignePanier.LignePanierVelo))]
    public virtual ICollection<LignePanier> ListeLignePaniers { get; set; } = new List<LignePanier>();

    [InverseProperty(nameof(MentionVelo.MentionVeloVelo))]
    public virtual ICollection<MentionVelo> ListeMentionVelos { get; set; } = new List<MentionVelo>();

    [InverseProperty(nameof(PhotoVelo.PhotoVeloVelo))]
    public virtual ICollection<PhotoVelo> ListePhotoVelos { get; set; } = new List<PhotoVelo>();

    [InverseProperty(nameof(TestVelo.TestVeloVelo))]
    public virtual ICollection<TestVelo> ListeTestVelos { get; set; } = new List<TestVelo>();

    [InverseProperty(nameof(Utilite.UtiliteVelo))]
    public virtual ICollection<Utilite> ListeUtilites { get; set; } = new List<Utilite>();

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Accessoire.ListeVelos))]
    public virtual ICollection<Accessoire> ListeAccessoires { get; set; } = new List<Accessoire>();

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Caracteristique.ListeVelos))]
    public virtual ICollection<Caracteristique> ListeCaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Magasin.ListeVelos))]
    public virtual ICollection<Magasin> ListeMagasins { get; set; } = new List<Magasin>();

    public long GetSize()
    {
        return sizeof(int) * 7 + sizeof(decimal) * 3 + NomVelo?.Length ?? 0 + TailleMin?.Length ??
            0 + TailleMax?.Length ?? 0 +
            NombreKms?.Length + DescriptifVelo?.Length ??
            0 + PositionMoteur?.Length ?? 0 + CapaciteBatterie?.Length ?? 0;
    }
}
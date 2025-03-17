using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_velo_vel", Schema = "upways")]
[Index(nameof(CaracteristiqueVeloId), Name = "ix_t_e_velo_vel_caracteristiqueveloid")]
[Index(nameof(CategorieId), Name = "ix_t_e_velo_vel_categorieid")]
[Index(nameof( MarqueId), Name = "ix_t_e_velo_vel_marqueid")]
[Index(nameof(MoteurId), Name = "ix_t_e_velo_vel_moteurid")]
public partial class Velo
{

    public Velo() 
    {
        ListeAlerteVelos = new HashSet<Alertevelo>();
        ListeEstRealises = new HashSet<Estrealise>();
        ListeLignePaniers = new HashSet<Lignepanier>();
        ListeMentionVelos = new HashSet<Mentionvelo>();
        ListePhotoVelos = new HashSet<Photovelo>();
        ListeTestVelos = new HashSet<Testvelo>();
        ListeMagasins = new HashSet<Magasin>();
        ListeCaracteristiques = new HashSet<Caracteristique>();
        ListeAccessoires = new HashSet<Accessoire>();
        ListeUtilites = new HashSet<Utilite>();
    }
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
    public int? AnneeVelo { get; set; }

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

    [Column("vel_descriptif", TypeName="text")]
    [StringLength(5000) ]
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
    [InverseProperty(nameof(Caracteristiquevelo.ListeVelos))]
    public virtual Caracteristiquevelo? VeloCaracteristiqueVelo { get; set; }

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty(nameof(Categorie.ListeVelos))]
    public virtual Categorie VeloCategorie { get; set; } = null!;

    [ForeignKey(nameof(MarqueId))]
    [InverseProperty(nameof(Marque.ListeVelos))]
    public virtual Marque? VeloMarque { get; set; }

    [ForeignKey(nameof(MoteurId))]
    [InverseProperty(nameof(Moteur.ListeVelos))]
    public virtual Moteur? VeloMoteur { get; set; }

    [InverseProperty(nameof(Alertevelo.AlerteVelo))]
    public virtual ICollection<Alertevelo> ListeAlerteVelos { get; set; } = new List<Alertevelo>();

    [InverseProperty(nameof(Estrealise.EstRealiseVelo))]
    public virtual ICollection<Estrealise> ListeEstRealises { get; set; } = new List<Estrealise>();

    [InverseProperty(nameof(Lignepanier.LignePanierVelo))]
    public virtual ICollection<Lignepanier> ListeLignePaniers { get; set; } = new List<Lignepanier>();

    [InverseProperty(nameof(Mentionvelo.MentionVeloVelo))]
    public virtual ICollection<Mentionvelo> ListeMentionVelos { get; set; } = new List<Mentionvelo>();

    [InverseProperty(nameof(Photovelo.PhotoVeloVelo))]
    public virtual ICollection<Photovelo> ListePhotoVelos { get; set; } = new List<Photovelo>();

    [InverseProperty(nameof(Testvelo.TestVeloVelo))]
    public virtual ICollection<Testvelo> ListeTestVelos { get; set; } = new List<Testvelo>();

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
}

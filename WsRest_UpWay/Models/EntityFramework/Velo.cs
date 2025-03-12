using Microsoft.CodeAnalysis.Elfie.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_velo_vel")]
public partial class Velo
{
    public Velo() 
    {
        CaracteristiqueveloNavigationId = new HashSet<Caracteristiquevelo>();
        IdmoteurNavigation = new HashSet<Moteur>();
    }
    [Key]
    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column ("vel_nom")]
    public string? NomVelo { get; set; }

    [Column ("vel_annee")]
    public decimal? AnneeVelo { get; set; }
    
    [Column("vel_min")]
    public string? TailleMin { get; set; }
    
    [Column("vel_max")]
    public string? TailleMax { get; set; }

    [Column("vel_km")]
    public string? NombreKms { get; set; }

    [Column("vel_prem")]
    public decimal? PrixRemise { get; set; }

    [Column("vel_pneuf")]
    public decimal? PrixNeuf { get; set; }

    [Column("vel_pred")]
    public decimal? PourcentageReduction { get; set; }

    [Column("vel_descr")]
    public string? DescriptifVelo { get; set; }

    [Column("vel_qnt")]
    public decimal? QuantiteVelo { get; set; }

    [Column("vel_pstn")]
    public string? PositionMoteur { get; set; }

    [Column("vel_capbat")]
    public string? CapaciteBatterie { get; set; }

    [Column("mrq_id")]
    public int? MarqueId { get; set; }

    [Column("cat_id")]
    public int CategorieId { get; set; }

    [Column("mot_id")]
    public int? MoteurId { get; set; }

    [Column("cat_id")]
    public int? CaracteristiqueveloId { get; set; }

    
    public virtual ICollection<Alertevelo> Alertevelos { get; set; } = new List<Alertevelo>();


    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();

    [ForeignKey(nameof(CaracteristiqueveloId))]
    [InverseProperty(nameof(Caracteristiquevelo.Velos))]
    public virtual Caracteristiquevelo? Caracteri { get; set; }

    [ForeignKey(nameof(CategorieId))]
    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    [ForeignKey(nameof(MarqueId))]
    public virtual Marque? IdmarqueNavigation { get; set; }

    [ForeignKey(nameof(MarqueId))]
    public virtual Moteur? IdmoteurNavigation { get; set; }

    [InverseProperty(nameof(Lignepanier.IdveloNavigation))]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();


    [InverseProperty(nameof(Mentionvelo.IdveloNavigation))]
    public virtual ICollection<Mentionvelo> Mentionvelos { get; set; } = new List<Mentionvelo>();

    [InverseProperty(nameof(Photovelo.IdveloNavigation))]
    public virtual ICollection<Photovelo> Photovelos { get; set; } = new List<Photovelo>();

    [InverseProperty(nameof(Testvelo.IdveloNavigation))]
    public virtual ICollection<Testvelo> Testvelos { get; set; } = new List<Testvelo>();
    
    [InverseProperty(nameof(Utilite.IdveloNavigation))]
    public virtual ICollection<Utilite> Utilites { get; set; } = new List<Utilite>();

    [InverseProperty(nameof(Accessoire.Idvelos))]
    public virtual ICollection<Accessoire> Idaccessoires { get; set; } = new List<Accessoire>();

    [InverseProperty(nameof(Caracteristique.Idvelos))]
    public virtual ICollection<Caracteristique> Idcaracteristiques { get; set; } = new List<Caracteristique>();

    [InverseProperty(nameof(Magasin.Idvelos))]
    public virtual ICollection<Magasin> Idmagasins { get; set; } = new List<Magasin>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("velo", Schema = "upways")]
[Index("Idcaracteristiquevelo", Name = "idx_velo_idcaracteristiquevelo")]
[Index("Idcategorie", Name = "idx_velo_idcategorie")]
[Index("Idmarque", Name = "idx_velo_idmarque")]
[Index("Idmoteur", Name = "idx_velo_idmoteur")]
public partial class Velo
{
    [Key]
    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("idmarque")]
    public int? Idmarque { get; set; }

    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("idmoteur")]
    public int? Idmoteur { get; set; }

    [Column("idcaracteristiquevelo")]
    public int? Idcaracteristiquevelo { get; set; }

    [Column("nomvelo")]
    [StringLength(200)]
    public string? Nomvelo { get; set; }

    [Column("anneevelo")]
    [Precision(4, 0)]
    public decimal? Anneevelo { get; set; }

    [Column("taillemin")]
    [StringLength(15)]
    public string? Taillemin { get; set; }

    [Column("taillemax")]
    [StringLength(15)]
    public string? Taillemax { get; set; }

    [Column("nombrekms")]
    [StringLength(15)]
    public string? Nombrekms { get; set; }

    [Column("prixremise")]
    [Precision(5, 0)]
    public decimal? Prixremise { get; set; }

    [Column("prixneuf")]
    [Precision(5, 0)]
    public decimal? Prixneuf { get; set; }

    [Column("pourcentagereduction")]
    [Precision(3, 0)]
    public decimal? Pourcentagereduction { get; set; }

    [Column("descriptifvelo")]
    [StringLength(5000)]
    public string? Descriptifvelo { get; set; }

    [Column("quantitevelo")]
    [Precision(3, 0)]
    public decimal? Quantitevelo { get; set; }

    [Column("positionmoteur")]
    [StringLength(20)]
    public string? Positionmoteur { get; set; }

    [Column("capacitebatterie")]
    [StringLength(10)]
    public string? Capacitebatterie { get; set; }

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

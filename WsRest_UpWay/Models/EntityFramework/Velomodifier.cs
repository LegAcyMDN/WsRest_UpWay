using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("velomodifier", Schema = "upways")]
[Index("Idcaracteristiquevelo", Name = "idx_velomodifier_idcaracteristiquevelomodifier")]
[Index("Idcategorie", Name = "idx_velomodifier_idcategorie")]
[Index("Idmarque", Name = "idx_velomodifier_idmarque")]
[Index("Idmoteur", Name = "idx_velomodifier_idmoteur")]
public partial class Velomodifier
{
    [Key]
    [Column("idvelom")]
    public int Idvelom { get; set; }

    [Column("idvelomodif")]
    public int Idvelomodif { get; set; }

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

    [Column("ancienprix")]
    [Precision(5, 0)]
    public decimal? Ancienprix { get; set; }

    [Column("modifier", TypeName = "timestamp without time zone")]
    public DateTime? Modifier { get; set; }

    [ForeignKey("Idcaracteristiquevelo")]
    [InverseProperty("Velomodifiers")]
    public virtual Caracteristiquevelo? IdcaracteristiqueveloNavigation { get; set; }

    [ForeignKey("Idcategorie")]
    [InverseProperty("Velomodifiers")]
    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    [ForeignKey("Idmarque")]
    [InverseProperty("Velomodifiers")]
    public virtual Marque? IdmarqueNavigation { get; set; }

    [ForeignKey("Idmoteur")]
    [InverseProperty("Velomodifiers")]
    public virtual Moteur? IdmoteurNavigation { get; set; }
}

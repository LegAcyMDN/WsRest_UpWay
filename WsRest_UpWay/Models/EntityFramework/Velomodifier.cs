using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_velomodofier_vlm", Schema = "upways")]
[Index("Idcaracteristiquevelo", Name = "idx_velomodifier_idcaracteristiquevelomodifier")]
[Index("Idcategorie", Name = "idx_velomodifier_idcategorie")]
[Index("Idmarque", Name = "idx_velomodifier_idmarque")]
[Index("Idmoteur", Name = "idx_velomodifier_idmoteur")]
public partial class Velomodifier
{
    [Key]
    [Column("vlm_id")]
    public int Idvelom { get; set; }

    [Column("vlm_idm")]
    public int Idvelomodif { get; set; }

    [Column("mar_id")]
    public int? Idmarque { get; set; }

    [Column("cat_id")]
    public int Idcategorie { get; set; }

    [Column("mot_id")]
    public int? Idmoteur { get; set; }

    [Column("car_id")]
    public int? Idcaracteristiquevelo { get; set; }

    [Column("vlm_nom")]
    [StringLength(200)]
    public string? Nomvelo { get; set; }

    [Column("vlm_annee")]
    [Precision(4, 0)]
    public decimal? Anneevelo { get; set; }

    [Column("vlm_taillemin")]
    [StringLength(15)]
    public string? Taillemin { get; set; }

    [Column("vlm_taillemax")]
    [StringLength(15)]
    public string? Taillemax { get; set; }

    [Column("vlm_kms")]
    [StringLength(15)]
    public string? Nombrekms { get; set; }

    [Column("vlm_prixremise")]
    [Precision(5, 0)]
    public decimal? Prixremise { get; set; }

    [Column("vlm_prixneuf")]
    [Precision(5, 0)]
    public decimal? Prixneuf { get; set; }

    [Column("vlm_pourcentagereduction")]
    [Precision(3, 0)]
    public decimal? Pourcentagereduction { get; set; }

    [Column("vlm_descriptif")]
    [StringLength(5000)]
    public string? Descriptifvelo { get; set; }

    [Column("vlm_quantite")]
    [Precision(3, 0)]
    public decimal? Quantitevelo { get; set; }

    [Column("vlm_positionmoteur")]
    [StringLength(20)]
    public string? Positionmoteur { get; set; }

    [Column("vlm_capacitebatterie")]
    [StringLength(10)]
    public string? Capacitebatterie { get; set; }

    [Column("vlm_ancienprix")]
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

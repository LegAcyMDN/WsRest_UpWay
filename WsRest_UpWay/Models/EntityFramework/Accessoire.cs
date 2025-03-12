using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_accessoire_acs", Schema = "upways")]
[Index("Idcategorie", Name = "idx_accessoire_idcategorie")]
[Index("Idmarque", Name = "idx_accessoire_idmarque")]
public partial class Accessoire
{
    [Key]
    [Column("acs_id")]
    public int Idaccessoire { get; set; }

    [Column("mar_id")]
    public int Idmarque { get; set; }

    [Column("cat_id")]
    public int Idcategorie { get; set; }

    [Column("acs_nom")]
    [StringLength(100)]
    public string? Nomaccessoire { get; set; }

    [Column("acs_prix", TypeName = "numeric(3, 2)")]
    public decimal? Prixaccessoire { get; set; }

    [Column("acs_description")]
    [StringLength(4096)]
    public string? Descriptionaccessoire { get; set; }

    [InverseProperty("IdaccessoireNavigation")]
    public virtual ICollection<Ajouteraccessoire> Ajouteraccessoires { get; set; } = new List<Ajouteraccessoire>();

    [ForeignKey("Idcategorie")]
    [InverseProperty("Accessoires")]
    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    [ForeignKey("Idmarque")]
    [InverseProperty("Accessoires")]
    public virtual Marque IdmarqueNavigation { get; set; } = null!;

    [InverseProperty("IdaccessoireNavigation")]
    public virtual ICollection<Photoaccessoire> Photoaccessoires { get; set; } = new List<Photoaccessoire>();

    [ForeignKey("Idaccessoire")]
    [InverseProperty("Idaccessoires")]
    public virtual ICollection<Velo> Idvelos { get; set; } = new List<Velo>();
}

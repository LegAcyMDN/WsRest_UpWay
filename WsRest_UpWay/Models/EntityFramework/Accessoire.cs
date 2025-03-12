using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("accessoire", Schema = "upways")]
[Index("Idcategorie", Name = "idx_accessoire_idcategorie")]
[Index("Idmarque", Name = "idx_accessoire_idmarque")]
public partial class Accessoire
{
    [Key]
    [Column("idaccessoire")]
    public int Idaccessoire { get; set; }

    [Column("idmarque")]
    public int Idmarque { get; set; }

    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("nomaccessoire")]
    [StringLength(100)]
    public string? Nomaccessoire { get; set; }

    [Column("prixaccessoire")]
    public int? Prixaccessoire { get; set; }

    [Column("descriptionaccessoire")]
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

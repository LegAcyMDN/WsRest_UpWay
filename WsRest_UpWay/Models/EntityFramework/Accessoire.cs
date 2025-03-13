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
    public int AccessoireId { get; set; }

    [Column("mar_id")]
    public int MarqueId { get; set; }

    [Column("cat_id")]
    public int CategorieId { get; set; }

    [Column("acs_nom")]
    [StringLength(100)]
    public string? NomAccessoire { get; set; }

    [Column("acs_prix", TypeName = "numeric(3, 2)")]
    public decimal? PrixAccessoire { get; set; }

    [Column("acs_description")]
    [StringLength(4096)]
    public string? DescriptionAccessoire { get; set; }

    [ForeignKey("Idcategorie")]
    [InverseProperty("Accessoires")]
    public virtual Categorie AccessoireCategorie { get; set; } = null!;

    [ForeignKey("Idmarque")]
    [InverseProperty("Accessoires")]
    public virtual Marque AccessoireMarque { get; set; } = null!;

    [InverseProperty("IdaccessoireNavigation")]
    public virtual ICollection<Ajouteraccessoire> ListeAjoutAccessoires { get; set; } = new List<Ajouteraccessoire>();

    [InverseProperty("IdaccessoireNavigation")]
    public virtual ICollection<Photoaccessoire> ListePhotoAccessoires { get; set; } = new List<Photoaccessoire>();

    [ForeignKey("Idaccessoire")]
    [InverseProperty("Idaccessoires")]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_accessoire_acs")]
public partial class Accessoire
{
    public Accessoire()
    {
        ListeAccessoires = new HashSet<Ajouteraccessoire>();
        ListePhotos = new HashSet<Photoaccessoire>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key]
    [Column("acs_id")]
    public int AccessoireId { get; set; }

    [Column("acs_nom")]
    [StringLength(100)]
    public string? NomAccessoire { get; set; }

    [Column("acs_prix", TypeName = "numeric(4, 2)")]
    public decimal? PrixAccessoire { get; set; }

    [Column("acs_description", TypeName = "text(1000)")]
    [StringLength(1000)]
    public string? DescriptionAccessoire { get; set; }

    [Key]
    [Column("mar_id")]
    public int MarqueId { get; set; }

    [Key]
    [Column("cat_id")]
    public int CategorieId { get; set; }

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty("")]
    public virtual Categorie CategorieAccessoire { get; set; } = null!;

    [ForeignKey(nameof(MarqueId))]
    [InverseProperty("")]
    public virtual Marque MarqueAccessoire { get; set; } = null!;

    public virtual ICollection<Ajouteraccessoire> ListeAccessoires { get; set; } = new List<Ajouteraccessoire>();

    public virtual ICollection<Photoaccessoire> ListePhotos { get; set; } = new List<Photoaccessoire>();

    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

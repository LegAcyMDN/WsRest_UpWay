using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_accessoire_acs", Schema = "upways")]
[Index(nameof(CategorieId), Name = "ix_t_e_accessoire_acs_categorieid")]
[Index(nameof(MarqueId), Name = "ix_t_e_accessoire_acs_marqueid")]
public partial class Accessoire
{
    public Accessoire() 
    {
        ListeAjoutAccessoires = new HashSet<Ajouteraccessoire>();
        ListePhotoAccessoires = new HashSet<Photoaccessoire>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key]
    [Column("acs_id")]
    public int AccessoireId { get; set; }

    [Key]
    [Column("mar_id")]
    public int MarqueId { get; set; }

    [Key]
    [Column("cat_id")]
    public int CategorieId { get; set; }

    [Column("acs_nom")]
    [StringLength(100)]
    public string? NomAccessoire { get; set; }

    [Column("acs_prix", TypeName = "numeric(4, 2)")]
    [Precision(4, 2)]
    public decimal? PrixAccessoire { get; set; }

    [Column("acs_description", TypeName = "text")]
    [StringLength(4096)]
    public string? DescriptionAccessoire { get; set; }

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty(nameof(Categorie.ListeAccessoires))]
    public virtual Categorie AccessoireCategorie { get; set; } = null!;

    [ForeignKey(nameof(MarqueId))]
    [InverseProperty(nameof(Marque.ListeAccessoires))]
    public virtual Marque AccessoireMarque { get; set; } = null!;

    [InverseProperty(nameof(Ajouteraccessoire.AjoutDAccessoire))]
    public virtual ICollection<Ajouteraccessoire> ListeAjoutAccessoires { get; set; } = new List<Ajouteraccessoire>();

    [InverseProperty(nameof(Photoaccessoire.PhotoAccessoireAccessoire))]
    public virtual ICollection<Photoaccessoire> ListePhotoAccessoires { get; set; } = new List<Photoaccessoire>();

    [ForeignKey(nameof(AccessoireId))]
    [InverseProperty(nameof(Velo.ListeAccessoires))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

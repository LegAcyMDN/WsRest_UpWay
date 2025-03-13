using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_categorie_cat", Schema = "upways")]
[Index("Libellecategorie", Name = "idx_catgegorie_libellecategorie")]
public partial class Categorie
{
    [Key]
    [Column("cat_id")]
    public int CategorieId { get; set; }

    [Column("cat_libelle")]
    [StringLength(100)]
    public string? LibelleCategorie { get; set; }

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Accessoire> ListeAccessoires { get; set; } = new List<Accessoire>();

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Velomodifier> ListeVeloModifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();

    [ForeignKey("Idcategorie")]
    [InverseProperty("Idcategories")]
    public virtual ICollection<Caracteristique> ListeCaracteristiques { get; set; } = new List<Caracteristique>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_categorie_cat", Schema = "upways")]
[Index(nameof(LibelleCategorie), Name = "ix_t_e_categorie_cat_libellecategorie")]
public partial class Categorie
{
    public Categorie()
    {
        ListeAccessoires = new HashSet<Accessoire>();
        ListeVeloModifiers = new HashSet<Velomodifier>();
        ListeVelos = new HashSet<Velo>();
        ListeCaracteristiques = new HashSet<Caracteristique>();
    }

    [Key]
    [Column("cat_id")]
    public int CategorieId { get; set; }

    [Column("cat_libelle")]
    [StringLength(100)]
    public string? LibelleCategorie { get; set; }

    [InverseProperty(nameof(Accessoire.AccessoireCategorie))]
    public virtual ICollection<Accessoire> ListeAccessoires { get; set; } = new List<Accessoire>();

    [InverseProperty(nameof(Velomodifier.VeloModifierCategorie))]
    public virtual ICollection<Velomodifier> ListeVeloModifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty(nameof(Velo.VeloCategorie))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty(nameof(Caracteristique.ListeCategories))]
    public virtual ICollection<Caracteristique> ListeCaracteristiques { get; set; } = new List<Caracteristique>();
}

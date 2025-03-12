using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("categorie", Schema = "upways")]
[Index("Libellecategorie", Name = "idx_catgegorie_libellecategorie")]
public partial class Categorie
{
    [Key]
    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("libellecategorie")]
    [StringLength(100)]
    public string? Libellecategorie { get; set; }

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Accessoire> Accessoires { get; set; } = new List<Accessoire>();

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();

    [ForeignKey("Idcategorie")]
    [InverseProperty("Idcategories")]
    public virtual ICollection<Caracteristique> Idcaracteristiques { get; set; } = new List<Caracteristique>();
}

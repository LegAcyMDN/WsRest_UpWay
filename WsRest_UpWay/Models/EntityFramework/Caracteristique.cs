using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("caracteristique", Schema = "upways")]
public partial class Caracteristique
{
    [Key]
    [Column("idcaracteristique")]
    public int Idcaracteristique { get; set; }

    [Column("libellecaracteristique")]
    [StringLength(100)]
    public string? Libellecaracteristique { get; set; }

    [Column("imagecaracteristique")]
    [StringLength(200)]
    public string? Imagecaracteristique { get; set; }

    [ForeignKey("Idcaracteristique")]
    [InverseProperty("Idcaracteristiques")]
    public virtual ICollection<Caracteristique> CarIdcaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey("CarIdcaracteristique")]
    [InverseProperty("CarIdcaracteristiques")]
    public virtual ICollection<Caracteristique> Idcaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey("Idcaracteristique")]
    [InverseProperty("Idcaracteristiques")]
    public virtual ICollection<Categorie> Idcategories { get; set; } = new List<Categorie>();

    [ForeignKey("Idcaracteristique")]
    [InverseProperty("Idcaracteristiques")]
    public virtual ICollection<Velo> Idvelos { get; set; } = new List<Velo>();
}

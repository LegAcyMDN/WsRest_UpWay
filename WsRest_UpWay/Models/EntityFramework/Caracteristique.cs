using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_caracteristique_car", Schema = "upways")]
public partial class Caracteristique
{
    [Key]
    [Column("car_id")]
    public int CaracteristiqueId { get; set; }

    [Column("car_libelle")]
    [StringLength(100)]
    public string? LibelleCaracteristique { get; set; }

    [Column("car_image")]
    [StringLength(200)]
    public string? ImageCaracteristique { get; set; }

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(Caracteristique.ListeCaracteristiques))]
    public virtual ICollection<Caracteristique> ListeSousCaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(Caracteristique.ListeSousCaracteristiques))]
    public virtual ICollection<Caracteristique> ListeCaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(Categorie.ListeCaracteristiques))]
    public virtual ICollection<Categorie> ListeCategories { get; set; } = new List<Categorie>();

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(Velo.ListeCaracteristiques))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_marque_mar", Schema = "upways")]
[Index("Nommarque", Name = "nommarque_unq", IsUnique = true)]
public partial class Marque
{
    [Key]
    [Column("mar_id")]
    public int MarqueId { get; set; }

    [Column("mar_nom")]
    [StringLength(100)]
    public string? NomMarque { get; set; }

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Accessoire> ListeAccessoires { get; set; } = new List<Accessoire>();

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Moteur> ListeMoteurs { get; set; } = new List<Moteur>();

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Velomodifier> ListeVeloModifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

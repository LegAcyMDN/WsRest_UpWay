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
    public int Idmarque { get; set; }

    [Column("mar_nom")]
    [StringLength(100)]
    public string? Nommarque { get; set; }

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Accessoire> Accessoires { get; set; } = new List<Accessoire>();

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Moteur> Moteurs { get; set; } = new List<Moteur>();

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty("IdmarqueNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}

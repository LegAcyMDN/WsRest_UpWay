using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("marque", Schema = "upways")]
[Index("Nommarque", Name = "nommarque_unq", IsUnique = true)]
public partial class Marque
{
    [Key]
    [Column("idmarque")]
    public int Idmarque { get; set; }

    [Column("nommarque")]
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

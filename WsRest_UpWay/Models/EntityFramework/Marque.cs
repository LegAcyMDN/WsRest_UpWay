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
    public Marque()
    {
        ListeAccessoires = new HashSet<Accessoire>();
        ListeMoteurs = new HashSet<Moteur>();
        ListeVeloModifiers = new HashSet<Velomodifier>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key]
    [Column("mar_id")]
    public int MarqueId { get; set; }

    [Column("mar_nom")]
    [StringLength(100)]
    public string? NomMarque { get; set; }

    [InverseProperty(nameof(Accessoire.AccessoireMarque))]
    public virtual ICollection<Accessoire> ListeAccessoires { get; set; } = new List<Accessoire>();

    [InverseProperty(nameof(Moteur.MoteurMarque))]
    public virtual ICollection<Moteur> ListeMoteurs { get; set; } = new List<Moteur>();

    [InverseProperty(nameof(Velomodifier.VeloModifierMarque))]
    public virtual ICollection<Velomodifier> ListeVeloModifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty(nameof(Velo.VeloMarque))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();
}

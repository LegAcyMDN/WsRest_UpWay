using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey(nameof(AccessoireId), nameof(PanierId))]
[Table("t_j_ajouteraccessoire_aja", Schema = "upways")]
[Index(nameof(AccessoireId), Name = "ix_t_e_ajouteracessoire_aja_accessoireid")]
[Index(nameof(PanierId), Name = "ix_t_e_ajouteracessoire_aja_panierid")]
public partial class Ajouteraccessoire
{
    [Key]
    [Column("acs_id")]
    public int AccessoireId { get; set; }

    [Key]
    [Column("pan_id")]
    public int PanierId { get; set; }

    [Column("aja_quantite")]
    [Precision(2, 0)]
    public int? QuantiteAccessoire { get; set; }

    [ForeignKey(nameof(AccessoireId))]
    [InverseProperty(nameof(Accessoire.ListeAjoutAccessoires))]
    public virtual Accessoire AjoutDAccessoire { get; set; } = null!;

    [ForeignKey(nameof(PanierId))]
    [InverseProperty(nameof(Panier.ListeAjouterAccessoires))]
    public virtual Panier AjoutDAccessoirePanier { get; set; } = null!;
}

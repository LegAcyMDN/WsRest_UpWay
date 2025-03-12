using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_j_ajouteraccessoire_aja")]
public partial class Ajouteraccessoire
{
    [Column("aja_quantite")]
    public int Quantiteaccessoire { get; set; }

    [Key]
    [Column("acs_id")]
    public int AccessoireId { get; set; }

    [Key]
    [Column("pan_id")]
    public int PanierId { get; set; }

    [ForeignKey(nameof(AccessoireId))]
    [InverseProperty(nameof(Accessoire.ListeAccessoires))]
    public virtual Accessoire AccessoireAjouter { get; set; } = null!;

    [ForeignKey(nameof(PanierId))]
    [InverseProperty("")]
    public virtual Panier PanierAccessoire { get; set; } = null!;
}
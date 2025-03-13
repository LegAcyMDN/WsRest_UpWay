using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idaccessoire", "Idpanier")]
[Table("t_j_ajouteraccessoire_aja", Schema = "upways")]
[Index("Idaccessoire", Name = "idx_ajouteracessoire_idaccessoire")]
[Index("Idpanier", Name = "idx_ajouteracessoire_idpanier")]
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

    [ForeignKey("Idaccessoire")]
    [InverseProperty("Ajouteraccessoires")]
    public virtual Accessoire AjoutDAccessoire { get; set; } = null!;

    [ForeignKey("Idpanier")]
    [InverseProperty("Ajouteraccessoires")]
    public virtual Panier AjoutDAccessoirePanier { get; set; } = null!;
}

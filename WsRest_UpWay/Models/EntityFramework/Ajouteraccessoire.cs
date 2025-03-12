using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idaccessoire", "Idpanier")]
[Table("ajouteraccessoire", Schema = "upways")]
[Index("Idaccessoire", Name = "idx_ajouteracessoire_idaccessoire")]
[Index("Idpanier", Name = "idx_ajouteracessoire_idpanier")]
public partial class Ajouteraccessoire
{
    [Key]
    [Column("idaccessoire")]
    public int Idaccessoire { get; set; }

    [Key]
    [Column("idpanier")]
    public int Idpanier { get; set; }

    [Column("quantiteaccessoire")]
    [Precision(2, 0)]
    public decimal Quantiteaccessoire { get; set; }

    [ForeignKey("Idaccessoire")]
    [InverseProperty("Ajouteraccessoires")]
    public virtual Accessoire IdaccessoireNavigation { get; set; } = null!;

    [ForeignKey("Idpanier")]
    [InverseProperty("Ajouteraccessoires")]
    public virtual Panier IdpanierNavigation { get; set; } = null!;
}

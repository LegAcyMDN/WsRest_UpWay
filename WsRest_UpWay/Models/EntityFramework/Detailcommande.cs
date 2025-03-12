using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("detailcommande", Schema = "upways")]
[Index("Idadressefact", Name = "idx_detailcommande_idadressefact")]
[Index("Idclient", Name = "idx_detailcommande_idclient")]
[Index("Idetatcommande", Name = "idx_detailcommande_idetatcommande")]
[Index("Idpanier", Name = "idx_detailcommande_idpanier")]
[Index("Idretraitmagasin", Name = "idx_detailcommande_idretraitmagasin")]
public partial class Detailcommande
{
    [Key]
    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("idretraitmagasin")]
    public int? Idretraitmagasin { get; set; }

    [Column("idadressefact")]
    public int? Idadressefact { get; set; }

    [Column("idetatcommande")]
    public int? Idetatcommande { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idpanier")]
    public int? Idpanier { get; set; }

    [Column("moyenpaiement")]
    [StringLength(10)]
    public string? Moyenpaiement { get; set; }

    [Column("modeexpedition")]
    [StringLength(20)]
    public string? Modeexpedition { get; set; }

    [Column("dateachat")]
    public DateOnly? Dateachat { get; set; }

    [ForeignKey("Idadressefact")]
    [InverseProperty("Detailcommandes")]
    public virtual Adressefacturation? IdadressefactNavigation { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Detailcommandes")]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idetatcommande")]
    [InverseProperty("Detailcommandes")]
    public virtual Etatcommande? IdetatcommandeNavigation { get; set; }

    [ForeignKey("Idpanier")]
    [InverseProperty("Detailcommandes")]
    public virtual Panier? IdpanierNavigation { get; set; }

    [ForeignKey("Idretraitmagasin")]
    [InverseProperty("Detailcommandes")]
    public virtual Retraitmagasin? IdretraitmagasinNavigation { get; set; }

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Panier> Paniers { get; set; } = new List<Panier>();

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Retraitmagasin> Retraitmagasins { get; set; } = new List<Retraitmagasin>();
}

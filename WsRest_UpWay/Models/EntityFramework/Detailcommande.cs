using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_detailcommande_detcom", Schema = "upways")]
[Index("Idadressefact", Name = "idx_detailcommande_idadressefact")]
[Index("Idclient", Name = "idx_detailcommande_idclient")]
[Index("Idetatcommande", Name = "idx_detailcommande_idetatcommande")]
[Index("Idpanier", Name = "idx_detailcommande_idpanier")]
[Index("Idretraitmagasin", Name = "idx_detailcommande_idretraitmagasin")]
public partial class Detailcommande
{
    [Key]
    [Column("detcom_id")]
    public int Idcommande { get; set; }

    [Column("retmag_id")]
    public int? Idretraitmagasin { get; set; }

    [Column("adfact_id")]
    public int? Idadressefact { get; set; }

    [Column("etacom_id")]
    public int? Idetatcommande { get; set; }

    [Column("clt_id")]
    public int Idclient { get; set; }

    [Column("pan_id")]
    public int? Idpanier { get; set; }

    [Column("detcom_moypai")]
    [StringLength(10)]
    public string? Moyenpaiement { get; set; }

    [Column("detcom_modexp")]
    [StringLength(20)]
    public string? Modeexpedition { get; set; }

    [Column("detcom_datach")]
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

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
    public int CommandeId { get; set; }

    [Column("retmag_id")]
    public int? RetraitMagasinId { get; set; }

    [Column("adfact_id")]
    public int? AdresseFactId { get; set; }

    [Column("etacom_id")]
    public int? EtatCommandeId { get; set; }

    [Column("clt_id")]
    public int ClientId { get; set; }

    [Column("pan_id")]
    public int? PanierId { get; set; }

    [Column("detcom_moypai")]
    [StringLength(10)]
    public string? MoyenPaiement { get; set; }

    [Column("detcom_modexp")]
    [StringLength(20)]
    public string? ModeExpedition { get; set; }

    [Column("detcom_datach", TypeName = "date")]
    public DateTime? DateAchat { get; set; }

    [ForeignKey("Idadressefact")]
    [InverseProperty("Detailcommandes")]
    public virtual Adressefacturation? DetailComAdresseFact { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Detailcommandes")]
    public virtual Compteclient DetailCommandeClient { get; set; } = null!;

    [ForeignKey("Idetatcommande")]
    [InverseProperty("Detailcommandes")]
    public virtual Etatcommande? DetailCommandeEtat { get; set; }

    [ForeignKey("Idpanier")]
    [InverseProperty("Detailcommandes")]
    public virtual Panier? DetailCommandePanier { get; set; }

    [ForeignKey("Idretraitmagasin")]
    [InverseProperty("Detailcommandes")]
    public virtual Retraitmagasin? DetailComRetraitMagasin { get; set; }

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Panier> ListePaniers { get; set; } = new List<Panier>();

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Retraitmagasin> ListeRetraitMagasins { get; set; } = new List<Retraitmagasin>();
}

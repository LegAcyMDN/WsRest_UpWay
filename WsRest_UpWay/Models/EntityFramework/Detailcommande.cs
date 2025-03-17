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
    public Detailcommande()
    {
        ListePaniers = new HashSet<Panier>();
        ListeRetraitMagasins = new HashSet<Retraitmagasin>();
    }

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
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "la date doit étre en format français")]
    public DateTime? DateAchat { get; set; }

    [ForeignKey(nameof(AdresseFactId))]
    [InverseProperty(nameof(Adressefacturation.ListeDetailCommande))]
    public virtual Adressefacturation? DetailComAdresseFact { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Compteclient.ListeDetailCommandes))]
    public virtual Compteclient DetailCommandeClient { get; set; } = null!;

    [ForeignKey(nameof(EtatCommandeId))]
    [InverseProperty(nameof(Etatcommande.ListeDetailCommandes))]
    public virtual Etatcommande? DetailCommandeEtat { get; set; }

    [ForeignKey(nameof(PanierId))]
    [InverseProperty(nameof(Panier.ListeDetailCommandes))]
    public virtual Panier? DetailCommandePanier { get; set; }

    [ForeignKey(nameof(RetraitMagasinId))]
    [InverseProperty(nameof(Retraitmagasin.ListeDetailCommandes))]
    public virtual Retraitmagasin? DetailComRetraitMagasin { get; set; }

    [InverseProperty(nameof(Panier.PanierDetailCommande))]
    public virtual ICollection<Panier> ListePaniers { get; set; } = new List<Panier>();

    [InverseProperty(nameof(Retraitmagasin.RetraitMagasinDetailCom))]
    public virtual ICollection<Retraitmagasin> ListeRetraitMagasins { get; set; } = new List<Retraitmagasin>();
}

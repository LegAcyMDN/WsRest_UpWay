using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_detailcommande_detcom", Schema = "upways")]
[Index(nameof(AdresseFactId), Name = "ix_t_e_detailcommande_detcom_adressefactid")]
[Index(nameof(ClientId), Name = "ix_t_e_detailcommande_detcom_clientid")]
[Index(nameof(EtatCommandeId), Name = "ix_t_e_detailcommande_detcom_etatcommandeid")]
[Index(nameof(PanierId), Name = "ix_t_e_detailcommande_detcom_panierid")]
[Index(nameof(RetraitMagasinId), Name = "ix_t_e_detailcommande_detcom_retraitmagasinid")]
public partial class DetailCommande
{
    public DetailCommande()
    {
        ListePaniers = new HashSet<Panier>();
        ListeRetraitMagasins = new HashSet<RetraitMagasin>();
    }

    [Key]
    [Column("detcom_id")]
    public int CommandeId { get; set; }

    [Column("retmag_id")]
    public int? RetraitMagasinId { get; set; }

    [Column("adf_id")]
    public int? AdresseFactId { get; set; }

    [Column("etacom_id")]
    public int? EtatCommandeId { get; set; }

    [Column("cli_id")]
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

    [ForeignKey(nameof(AdresseFactId))]
    [InverseProperty(nameof(AdresseFacturation.ListeDetailCommande))]
    public virtual AdresseFacturation? DetailComAdresseFact { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(CompteClient.ListeDetailCommandes))]
    public virtual CompteClient DetailCommandeClient { get; set; } = null!;

    [ForeignKey(nameof(EtatCommandeId))]
    [InverseProperty(nameof(EtatCommande.ListeDetailCommandes))]
    public virtual EtatCommande? DetailCommandeEtat { get; set; }

    [ForeignKey(nameof(PanierId))]
    [InverseProperty(nameof(Panier.ListeDetailCommandes))]
    public virtual Panier? DetailCommandePanier { get; set; }

    [ForeignKey(nameof(RetraitMagasinId))]
    [InverseProperty(nameof(RetraitMagasin.ListeDetailCommandes))]
    public virtual RetraitMagasin? DetailComRetraitMagasin { get; set; }

    [InverseProperty(nameof(Panier.PanierDetailCommande))]
    public virtual ICollection<Panier> ListePaniers { get; set; } = new List<Panier>();

    [InverseProperty(nameof(RetraitMagasin.RetraitMagasinDetailCom))]
    public virtual ICollection<RetraitMagasin> ListeRetraitMagasins { get; set; } = new List<RetraitMagasin>();
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_detailcommande_detcom")]
public partial class Detailcommande
{
    [Key]
    [Column("detcom_id")]
    public int CommandeId { get; set; }

    [Key]
    [Column("retmag_id")]
    public int? RetraitmagasinId { get; set; }

    [Key]
    [Column("adfact_id")]
    public int? AdressefactId { get; set; }

    [Key]
    [Column("etacom_id")]
    public int? EtatcommandeId { get; set; }

    [Key]
    [Column("clt_id")]
    public int ClientId { get; set; }

    [Key]
    [Column("pan_id")]
    public int? PanierId { get; set; }

    [Key]
    [Column("detcom_moypai")]
    public string? Moyenpaiement { get; set; }

    [Key]
    [Column("detcom_modexp")]
    public string? Modeexpedition { get; set; }

    [Key]
    [Column("detcom_datacht")]
    public DateOnly? Dateachat { get; set; }

    [ForeignKey(nameof(AdressefactId))]
    [InverseProperty(nameof(Adressefacturation.CommandesAdresseFacturation))]
    public virtual Adressefacturation? AdressefactCommande { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Compteclient.CommandesClient))]
    public virtual Compteclient ClientCommandant { get; set; } = null!;

    [ForeignKey(nameof(EtatcommandeId))]
    [InverseProperty(nameof(Etatcommande.EtatCommandes))]
    public virtual Etatcommande? EtatCommande { get; set; }

    [ForeignKey(nameof(PanierId))]
    [InverseProperty(nameof(Panier.CommandesPanier))]
    public virtual Panier? PanierCommande { get; set; }

    [ForeignKey(nameof(RetraitmagasinId))]
    [InverseProperty(nameof(Retraitmagasin.CommandesRetraitMagasin))]
    public virtual Retraitmagasin? RetraitmagasinCommande { get; set; }

    [InverseProperty(nameof(Panier.CommandePanier))]
    public virtual ICollection<Panier> PaniersCommande { get; set; } = new List<Panier>();
    [InverseProperty(nameof(Retraitmagasin.CommandeRetraitmagasins))]
    public virtual ICollection<Retraitmagasin> RetraitmagasinsCommande { get; set; } = new List<Retraitmagasin>();
}

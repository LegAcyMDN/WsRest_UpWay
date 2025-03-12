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
    public Detailcommande()
    {
        RetraitmagasinsCommande = new HashSet<Retraitmagasin>();
        PaniersCommande = new HashSet<Panier>();
    }

    [Key]
    [Column("detcom_id")]
    public int CommandeId { get; set; }


    [Column("retmag_id")]
    public int? RetraitmagasinId { get; set; }


    [Column("adfact_id")]
    public int? AdressefactId { get; set; }


    [Column("etacom_id")]
    public int? EtatcommandeId { get; set; }


    [Column("clt_id")]
    public int ClientId { get; set; }


    [Column("pan_id")]
    public int? PanierId { get; set; }


    [Column("detcom_moypai")]
    [StringLength(100)]
    public string? Moyenpaiement { get; set; }


    [Column("detcom_modexp")]
    [StringLength(100)]
    public string? Modeexpedition { get; set; }


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

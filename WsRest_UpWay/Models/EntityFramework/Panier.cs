using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_panier_pan", Schema = "upways")]
[Index(nameof(CommandeId), Name = "ix_t_e_panier_pan_commandeid")]
public partial class Panier
{
    public Panier()
    {
        ListeAjouterAccessoires = new HashSet<Ajouteraccessoire>();
        ListeDetailCommandes = new HashSet<Detailcommande>();
        ListeInformations = new HashSet<Information>();
        ListeLignePaniers = new HashSet<Lignepanier>();
    }

    [Key]
    [Column("pan_id")]
    public int PanierId { get; set; }

    [Key]
    [Column("cli_id")]
    public int? ClientId { get; set; }

    [Key]
    [Column("com_id")]
    public int? CommandeId { get; set; }

    [Column("pan_cookie")]
    [StringLength(255)]
    public string? Cookie { get; set; }

    [Column("pan_prix")]
    [Precision(11, 2)]
    public decimal? PrixPanier { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Compteclient.ListePaniers))]
    public virtual Compteclient? PanierClient { get; set; }

    [ForeignKey(nameof(CommandeId))]
    [InverseProperty(nameof(Detailcommande.ListePaniers))]
    public virtual Detailcommande? PanierDetailCommande { get; set; }

    [InverseProperty(nameof(Ajouteraccessoire.AjoutDAccessoirePanier))]
    public virtual ICollection<Ajouteraccessoire> ListeAjouterAccessoires { get; set; } = new List<Ajouteraccessoire>();

    [InverseProperty(nameof(Detailcommande.DetailCommandePanier))]
    public virtual ICollection<Detailcommande> ListeDetailCommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty(nameof(Information.InformationPanier))]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();

    [InverseProperty(nameof(Lignepanier.LignePanierPanier))]
    public virtual ICollection<Lignepanier> ListeLignePaniers { get; set; } = new List<Lignepanier>();
}

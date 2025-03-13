using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_panier_pan", Schema = "upways")]
[Index("Idcommande", Name = "idx_panier_idcommannde")]
public partial class Panier
{
    [Key]
    [Column("pan_id")]
    public int PanierId { get; set; }

    [Column("cli_id")]
    public int? ClientId { get; set; }

    [Column("com_id")]
    public int? CommandeId { get; set; }

    [Column("pan_cookie")]
    [StringLength(255)]
    public string? Cookie { get; set; }

    [Column("pan_prix")]
    [Precision(11, 2)]
    public decimal? PrixPanier { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Paniers")]
    public virtual Compteclient? PanierClient { get; set; }

    [ForeignKey("Idcommande")]
    [InverseProperty("Paniers")]
    public virtual Detailcommande? PanierDetailCommande { get; set; }

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Ajouteraccessoire> ListeAjouterAccessoires { get; set; } = new List<Ajouteraccessoire>();

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Detailcommande> ListeDetailCommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Lignepanier> ListeLignePaniers { get; set; } = new List<Lignepanier>();
}

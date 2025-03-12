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
    public int Idpanier { get; set; }

    [Column("cli_id")]
    public int? Idclient { get; set; }

    [Column("com_id")]
    public int? Idcommande { get; set; }

    [Column("pan_cookie")]
    [StringLength(255)]
    public string? Cookie { get; set; }

    [Column("pan_prix")]
    [Precision(11, 2)]
    public decimal? Prixpanier { get; set; }

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Ajouteraccessoire> Ajouteraccessoires { get; set; } = new List<Ajouteraccessoire>();

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [ForeignKey("Idclient")]
    [InverseProperty("Paniers")]
    public virtual Compteclient? IdclientNavigation { get; set; }

    [ForeignKey("Idcommande")]
    [InverseProperty("Paniers")]
    public virtual Detailcommande? IdcommandeNavigation { get; set; }

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Information> Information { get; set; } = new List<Information>();

    [InverseProperty("IdpanierNavigation")]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}

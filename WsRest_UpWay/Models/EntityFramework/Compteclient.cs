using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_compteclient_coc", Schema = "upways")]
[Index(nameof(EmailClient), Name = "ix_t_e_compteclient_coc_email_unq", IsUnique = true)]
[Index(nameof(LoginClient), Name = "ix_t_e_compteclient_coc_pseudo_unq", IsUnique = true)]
public partial class Compteclient
{
    [Key]
    [Column("coc_id")]
    public int ClientId { get; set; }

    [Column("coc_login")]
    [StringLength(20)]
    public string? LoginClient { get; set; }

    [Column("coc_MPD")]
    [StringLength(64)]
    public string? MotDePasseClient { get; set; }

    [Column("coc_email")]
    [StringLength(200)]
    public string? EmailClient { get; set; }

    [Column("coc_prenom")]
    [StringLength(20)]
    public string? PrenomClient { get; set; }

    [Column("coc_nom")]
    [StringLength(30)]
    public string? NomClient { get; set; }

    [Column("coc_dateCreation", TypeName = "date")]
    public DateOnly? DateCreation { get; set; }

    [Column("coc_remember_token")]
    [StringLength(100)]
    public string? RememberToken { get; set; }

    [Column("coc_two_factor_secret", TypeName = "text")]
    [StringLength(4096)]
    public string? TwoFactorSecret { get; set; }

    [Column("coc_two_factor_recovery_codes", TypeName = "text")]
    [StringLength(4096)]
    public string? TwoFactorRecoveryCodes { get; set; }

    [Column("coc_two_factor_confirmed_at", TypeName = "text")]
    [StringLength(4096)]
    public string? TwoFactorConfirmedAt { get; set; }

    [Column("coc_usertype")]
    [StringLength(20)]
    public string? Usertype { get; set; }

    [Column("coc_email_verified_at", TypeName ="date")]
    public DateOnly? EmailVerifiedAt { get; set; }

    [Column("coc_is_from_google")]
    public bool? IsFromGoogle { get; set; }

    [InverseProperty(nameof(Adresseexpedition.AdresseExpeClient))]
    public virtual ICollection<Adresseexpedition> ListeAdresseExpe { get; set; } = new List<Adresseexpedition>();

    [InverseProperty(nameof(Adressefacturation.AdresseFactClient))]
    public virtual ICollection<Adressefacturation> ListeAdresseFact { get; set; } = new List<Adressefacturation>();

    [InverseProperty(nameof(Alertevelo.AlerteClient))]
    public virtual ICollection<Alertevelo> ListeAlerteVelos { get; set; } = new List<Alertevelo>();

    [InverseProperty(nameof(Detailcommande.DetailCommandeClient))]
    public virtual ICollection<Detailcommande> ListeDetailCommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty(nameof(Panier.PanierClient))]
    public virtual ICollection<Panier> ListePaniers { get; set; } = new List<Panier>();
}

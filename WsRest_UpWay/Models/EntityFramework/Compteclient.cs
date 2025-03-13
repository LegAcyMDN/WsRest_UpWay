using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_compteclient_coc", Schema = "upways")]
[Index("Emailclient", Name = "email_unq", IsUnique = true)]
[Index("Loginclient", Name = "pseudo_unq", IsUnique = true)]
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

    [Column("coc_two_factor_confirmed_at", TypeName = "date")]
    public DateTime? TwoFactorConfirmedAt { get; set; }

    [Column("coc_usertype")]
    [StringLength(20)]
    public string? Usertype { get; set; }

    [Column("coc_email_verified_at", TypeName ="date")]
    public DateOnly? EmailVerifiedAt { get; set; }

    [Column("coc_is_from_google")]
    public bool? IsFromGoogle { get; set; }
    
    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Adresseexpedition> ListeAdresseExpe { get; set; } = new List<Adresseexpedition>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Adressefacturation> ListeAdresseFact { get; set; } = new List<Adressefacturation>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Alertevelo> ListeAlerteVelos { get; set; } = new List<Alertevelo>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Detailcommande> ListeDetailCommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Panier> ListePaniers { get; set; } = new List<Panier>();
}

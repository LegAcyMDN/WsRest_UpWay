using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("compteclient", Schema = "upways")]
[Index("Emailclient", Name = "email_unq", IsUnique = true)]
[Index("Loginclient", Name = "pseudo_unq", IsUnique = true)]
public partial class Compteclient
{
    [Key]
    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("loginclient")]
    [StringLength(20)]
    public string? Loginclient { get; set; }

    [Column("motdepasseclient")]
    [StringLength(64)]
    public string? Motdepasseclient { get; set; }

    [Column("emailclient")]
    [StringLength(200)]
    public string? Emailclient { get; set; }

    [Column("prenomclient")]
    [StringLength(20)]
    public string? Prenomclient { get; set; }

    [Column("nomclient")]
    [StringLength(30)]
    public string? Nomclient { get; set; }

    [Column("datecreation")]
    public DateOnly? Datecreation { get; set; }

    [Column("remember_token")]
    [StringLength(100)]
    public string? RememberToken { get; set; }

    [Column("two_factor_secret")]
    [StringLength(4096)]
    public string? TwoFactorSecret { get; set; }

    [Column("two_factor_recovery_codes")]
    [StringLength(4096)]
    public string? TwoFactorRecoveryCodes { get; set; }

    [Column("two_factor_confirmed_at")]
    [StringLength(4096)]
    public string? TwoFactorConfirmedAt { get; set; }

    [Column("usertype")]
    [StringLength(20)]
    public string? Usertype { get; set; }

    [Column("email_verified_at")]
    public DateOnly? EmailVerifiedAt { get; set; }

    [Column("is_from_google")]
    public bool? IsFromGoogle { get; set; }

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Adresseexpedition> Adresseexpeditions { get; set; } = new List<Adresseexpedition>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Adressefacturation> Adressefacturations { get; set; } = new List<Adressefacturation>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Alertevelo> Alertevelos { get; set; } = new List<Alertevelo>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Panier> Paniers { get; set; } = new List<Panier>();
}

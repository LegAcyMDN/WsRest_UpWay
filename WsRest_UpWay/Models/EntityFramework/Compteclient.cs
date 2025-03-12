using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Compteclient
{
    public int Idclient { get; set; }

    public string? Loginclient { get; set; }

    public string? Motdepasseclient { get; set; }

    public string? Emailclient { get; set; }

    public string? Prenomclient { get; set; }

    public string? Nomclient { get; set; }

    public DateOnly? Datecreation { get; set; }

    public string? RememberToken { get; set; }

    public string? TwoFactorSecret { get; set; }

    public string? TwoFactorRecoveryCodes { get; set; }

    public string? TwoFactorConfirmedAt { get; set; }

    public string? Usertype { get; set; }

    public DateOnly? EmailVerifiedAt { get; set; }

    public bool? IsFromGoogle { get; set; }

    public virtual ICollection<Adresseexpedition> Adresseexpeditions { get; set; } = new List<Adresseexpedition>();

    public virtual ICollection<Adressefacturation> Adressefacturations { get; set; } = new List<Adressefacturation>();

    public virtual ICollection<Alertevelo> Alertevelos { get; set; } = new List<Alertevelo>();

    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    public virtual ICollection<Panier> Paniers { get; set; } = new List<Panier>();
}

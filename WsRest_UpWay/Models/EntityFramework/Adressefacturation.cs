using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Adressefacturation
{
    public int Idadressefact { get; set; }

    public int Idclient { get; set; }

    public int? Idadresseexp { get; set; }

    public string? Paysfact { get; set; }

    public string? Appartementfact { get; set; }

    public string? Ruefact { get; set; }

    public string? Cpfact { get; set; }

    public string? Regionfact { get; set; }

    public string? Villefact { get; set; }

    public string? Telephonefact { get; set; }

    public virtual ICollection<Adresseexpedition> Adresseexpeditions { get; set; } = new List<Adresseexpedition>();

    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    public virtual Adresseexpedition? IdadresseexpNavigation { get; set; }

    public virtual Compteclient IdclientNavigation { get; set; } = null!;
}

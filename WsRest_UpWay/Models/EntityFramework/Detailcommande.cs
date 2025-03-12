using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Detailcommande
{
    public int Idcommande { get; set; }

    public int? Idretraitmagasin { get; set; }

    public int? Idadressefact { get; set; }

    public int? Idetatcommande { get; set; }

    public int Idclient { get; set; }

    public int? Idpanier { get; set; }

    public string? Moyenpaiement { get; set; }

    public string? Modeexpedition { get; set; }

    public DateOnly? Dateachat { get; set; }

    public virtual Adressefacturation? IdadressefactNavigation { get; set; }

    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    public virtual Etatcommande? IdetatcommandeNavigation { get; set; }

    public virtual Panier? IdpanierNavigation { get; set; }

    public virtual Retraitmagasin? IdretraitmagasinNavigation { get; set; }

    public virtual ICollection<Panier> Paniers { get; set; } = new List<Panier>();

    public virtual ICollection<Retraitmagasin> Retraitmagasins { get; set; } = new List<Retraitmagasin>();
}

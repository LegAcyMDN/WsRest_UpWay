using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Panier
{
    public int Idpanier { get; set; }

    public int? Idclient { get; set; }

    public int? Idcommande { get; set; }

    public string? Cookie { get; set; }

    public decimal? Prixpanier { get; set; }

    public virtual ICollection<Ajouteraccessoire> Ajouteraccessoires { get; set; } = new List<Ajouteraccessoire>();

    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    public virtual Compteclient? IdclientNavigation { get; set; }

    public virtual Detailcommande? IdcommandeNavigation { get; set; }

    public virtual ICollection<Information> Information { get; set; } = new List<Information>();

    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}

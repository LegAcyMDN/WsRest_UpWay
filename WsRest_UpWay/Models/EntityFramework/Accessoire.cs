using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Accessoire
{
    public int Idaccessoire { get; set; }

    public int Idmarque { get; set; }

    public int Idcategorie { get; set; }

    public string? Nomaccessoire { get; set; }

    public int? Prixaccessoire { get; set; }

    public string? Descriptionaccessoire { get; set; }

    public virtual ICollection<Ajouteraccessoire> Ajouteraccessoires { get; set; } = new List<Ajouteraccessoire>();

    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    public virtual Marque IdmarqueNavigation { get; set; } = null!;

    public virtual ICollection<Photoaccessoire> Photoaccessoires { get; set; } = new List<Photoaccessoire>();

    public virtual ICollection<Velo> Idvelos { get; set; } = new List<Velo>();
}

using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Velo
{
    public int Idvelo { get; set; }

    public int? Idmarque { get; set; }

    public int Idcategorie { get; set; }

    public int? Idmoteur { get; set; }

    public int? Idcaracteristiquevelo { get; set; }

    public string? Nomvelo { get; set; }

    public decimal? Anneevelo { get; set; }

    public string? Taillemin { get; set; }

    public string? Taillemax { get; set; }

    public string? Nombrekms { get; set; }

    public decimal? Prixremise { get; set; }

    public decimal? Prixneuf { get; set; }

    public decimal? Pourcentagereduction { get; set; }

    public string? Descriptifvelo { get; set; }

    public decimal? Quantitevelo { get; set; }

    public string? Positionmoteur { get; set; }

    public string? Capacitebatterie { get; set; }

    public virtual ICollection<Alertevelo> Alertevelos { get; set; } = new List<Alertevelo>();

    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();

    public virtual Caracteristiquevelo? IdcaracteristiqueveloNavigation { get; set; }

    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    public virtual Marque? IdmarqueNavigation { get; set; }

    public virtual Moteur? IdmoteurNavigation { get; set; }

    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();

    public virtual ICollection<Mentionvelo> Mentionvelos { get; set; } = new List<Mentionvelo>();

    public virtual ICollection<Photovelo> Photovelos { get; set; } = new List<Photovelo>();

    public virtual ICollection<Testvelo> Testvelos { get; set; } = new List<Testvelo>();

    public virtual ICollection<Utilite> Utilites { get; set; } = new List<Utilite>();

    public virtual ICollection<Accessoire> Idaccessoires { get; set; } = new List<Accessoire>();

    public virtual ICollection<Caracteristique> Idcaracteristiques { get; set; } = new List<Caracteristique>();

    public virtual ICollection<Magasin> Idmagasins { get; set; } = new List<Magasin>();
}

using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Velomodifier
{
    public int Idvelom { get; set; }

    public int Idvelomodif { get; set; }

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

    public decimal? Ancienprix { get; set; }

    public DateTime? Modifier { get; set; }

    public virtual Caracteristiquevelo? IdcaracteristiqueveloNavigation { get; set; }

    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    public virtual Marque? IdmarqueNavigation { get; set; }

    public virtual Moteur? IdmoteurNavigation { get; set; }
}

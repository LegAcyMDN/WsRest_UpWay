using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Vvelo
{
    public string? Nomvelo { get; set; }

    public decimal? Anneevelo { get; set; }

    public string? Taillemin { get; set; }

    public string? Taillemax { get; set; }

    public string? Nombrekms { get; set; }

    public decimal? Prixremise { get; set; }

    public decimal? Prixneuf { get; set; }

    public decimal? Pourcentagereduction { get; set; }

    public string? Descriptifvelo { get; set; }

    public string? Nommarque { get; set; }

    public string? Libellecategorie { get; set; }

    public string? Libellemention { get; set; }
}

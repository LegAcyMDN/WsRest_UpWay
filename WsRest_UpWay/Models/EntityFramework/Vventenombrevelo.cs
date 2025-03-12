using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Vventenombrevelo
{
    public int? Idvelo { get; set; }

    public string? Nomvelo { get; set; }

    public string? Libellecategorie { get; set; }

    public string? Nommarque { get; set; }

    public string? Taillemin { get; set; }

    public string? Taillemax { get; set; }

    public string? Modelemoteur { get; set; }

    public decimal? Prixpanier { get; set; }

    public string? Titreassurance { get; set; }

    public DateOnly? Dateachat { get; set; }

    public int? Idadressefact { get; set; }
}

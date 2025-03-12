using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Vventenombreacessoire
{
    public int? Idaccessoire { get; set; }

    public string? Nomaccessoire { get; set; }

    public string? Libellecategorie { get; set; }

    public decimal? Prixpanier { get; set; }

    public DateOnly? Dateachat { get; set; }

    public string? Nommarque { get; set; }

    public int? Idadressefact { get; set; }
}

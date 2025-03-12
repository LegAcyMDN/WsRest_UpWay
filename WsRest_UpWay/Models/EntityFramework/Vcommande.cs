using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Vcommande
{
    public int? Idvelo { get; set; }

    public string? Titreassurance { get; set; }

    public int? Idpanier { get; set; }

    public int? Idcommande { get; set; }

    public decimal? Prixpanier { get; set; }

    public int? Idclient { get; set; }
}

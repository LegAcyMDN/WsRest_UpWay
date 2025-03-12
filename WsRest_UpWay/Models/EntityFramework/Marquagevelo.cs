using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Marquagevelo
{
    public string Codemarquage { get; set; } = null!;

    public int Idpanier { get; set; }

    public int Idvelo { get; set; }

    public decimal? Prixmarquage { get; set; }

    public virtual Lignepanier Lignepanier { get; set; } = null!;
}

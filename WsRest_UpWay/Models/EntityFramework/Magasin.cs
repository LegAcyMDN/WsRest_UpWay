using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Magasin
{
    public int Idmagasin { get; set; }

    public string? Nommagasin { get; set; }

    public string? Horairemagasin { get; set; }

    public string? Ruemagasin { get; set; }

    public string? Cpmagasin { get; set; }

    public string? Villemagasin { get; set; }

    public virtual ICollection<Retraitmagasin> Retraitmagasins { get; set; } = new List<Retraitmagasin>();

    public virtual ICollection<Testvelo> Testvelos { get; set; } = new List<Testvelo>();

    public virtual ICollection<Velo> Idvelos { get; set; } = new List<Velo>();
}

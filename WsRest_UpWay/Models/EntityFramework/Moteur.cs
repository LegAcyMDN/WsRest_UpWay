using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Moteur
{
    public int Idmoteur { get; set; }

    public int Idmarque { get; set; }

    public string? Modelemoteur { get; set; }

    public string? Couplemoteur { get; set; }

    public string? Vitessemaximal { get; set; }

    public virtual Marque IdmarqueNavigation { get; set; } = null!;

    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}

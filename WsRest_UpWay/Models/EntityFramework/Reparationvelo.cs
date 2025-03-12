using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Reparationvelo
{
    public int Idreparation { get; set; }

    public bool? Checkreparation { get; set; }

    public bool? Checkvalidation { get; set; }

    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();
}

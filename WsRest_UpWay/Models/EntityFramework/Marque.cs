using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Marque
{
    public int Idmarque { get; set; }

    public string? Nommarque { get; set; }

    public virtual ICollection<Accessoire> Accessoires { get; set; } = new List<Accessoire>();

    public virtual ICollection<Moteur> Moteurs { get; set; } = new List<Moteur>();

    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}

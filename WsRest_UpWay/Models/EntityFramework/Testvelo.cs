using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Testvelo
{
    public int Idtest { get; set; }

    public int Idvelo { get; set; }

    public int Idmagasin { get; set; }

    public DateOnly? Datetest { get; set; }

    public TimeOnly? Heuretest { get; set; }

    public virtual Magasin IdmagasinNavigation { get; set; } = null!;

    public virtual Velo IdveloNavigation { get; set; } = null!;
}

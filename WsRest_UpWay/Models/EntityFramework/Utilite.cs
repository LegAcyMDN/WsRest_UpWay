using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Utilite
{
    public int Idutilite { get; set; }

    public int Idvelo { get; set; }

    public decimal? Valeurutilite { get; set; }

    public virtual Velo IdveloNavigation { get; set; } = null!;
}

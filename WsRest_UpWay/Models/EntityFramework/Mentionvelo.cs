using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Mentionvelo
{
    public int Idmention { get; set; }

    public int Idvelo { get; set; }

    public string? Libellemention { get; set; }

    public string? Valeurmention { get; set; }

    public virtual Velo IdveloNavigation { get; set; } = null!;
}

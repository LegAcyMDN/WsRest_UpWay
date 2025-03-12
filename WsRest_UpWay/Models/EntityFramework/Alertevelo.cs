using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Alertevelo
{
    public int Idalerte { get; set; }

    public int Idclient { get; set; }

    public int Idvelo { get; set; }

    public DateTime? Modifalerte { get; set; }

    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    public virtual Velo IdveloNavigation { get; set; } = null!;
}

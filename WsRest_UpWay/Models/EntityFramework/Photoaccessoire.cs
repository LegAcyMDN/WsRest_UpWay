using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Photoaccessoire
{
    public int Idphotoaccessoire { get; set; }

    public int Idaccessoire { get; set; }

    public byte[]? Urlphotoaccessoire { get; set; }

    public virtual Accessoire IdaccessoireNavigation { get; set; } = null!;
}

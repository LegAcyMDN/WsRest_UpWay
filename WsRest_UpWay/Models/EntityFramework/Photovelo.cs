using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Photovelo
{
    public int Idphotovelo { get; set; }

    public int Idvelo { get; set; }

    public string? Urlphotovelo { get; set; }

    public byte[]? Photobytea { get; set; }

    public virtual Velo IdveloNavigation { get; set; } = null!;
}

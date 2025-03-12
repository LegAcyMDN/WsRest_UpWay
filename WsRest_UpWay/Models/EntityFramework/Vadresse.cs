using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Vadresse
{
    public int? Idadressefact { get; set; }

    public string? Paysfact { get; set; }

    public string? Regionfact { get; set; }

    public string? Villefact { get; set; }
}

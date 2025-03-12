using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Dpo
{
    public int Iddpo { get; set; }

    public int? Idclient { get; set; }

    public string? Typeoperation { get; set; }

    public DateOnly? Daterequetedpo { get; set; }
}

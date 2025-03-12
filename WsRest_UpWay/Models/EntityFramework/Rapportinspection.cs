using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Rapportinspection
{
    public int Idinspection { get; set; }

    public string? Typeinspection { get; set; }

    public string? Soustypeinspection { get; set; }

    public string? Pointdinspection { get; set; }

    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();
}

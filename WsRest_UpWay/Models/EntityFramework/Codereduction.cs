using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Codereduction
{
    public string Idreduction { get; set; } = null!;

    public bool? Actifreduction { get; set; }

    public int? Reduction { get; set; }

    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}

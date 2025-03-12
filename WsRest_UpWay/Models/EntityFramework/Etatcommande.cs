using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Etatcommande
{
    public int Idetatcommande { get; set; }

    public string? Libelleetat { get; set; }

    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();
}

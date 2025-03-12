using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Retraitmagasin
{
    public int Idretraitmagasin { get; set; }

    public int? Idinformations { get; set; }

    public int? Idcommande { get; set; }

    public int Idmagasin { get; set; }

    public DateOnly? Dateretrait { get; set; }

    public TimeOnly? Heureretrait { get; set; }

    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    public virtual Detailcommande? IdcommandeNavigation { get; set; }

    public virtual Information? IdinformationsNavigation { get; set; }

    public virtual Magasin IdmagasinNavigation { get; set; } = null!;

    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}

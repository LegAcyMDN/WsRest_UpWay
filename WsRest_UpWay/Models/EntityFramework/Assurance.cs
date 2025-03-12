using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Assurance
{
    public int Idassurance { get; set; }

    public string? Titreassurance { get; set; }

    public string? Descriptionassurance { get; set; }

    public decimal? Prixassurance { get; set; }

    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}

using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Lignepanier
{
    public int Idpanier { get; set; }

    public int Idvelo { get; set; }

    public int Idassurance { get; set; }

    public decimal? Quantitepanier { get; set; }

    public decimal? Prixquantite { get; set; }

    public virtual Assurance IdassuranceNavigation { get; set; } = null!;

    public virtual Panier IdpanierNavigation { get; set; } = null!;

    public virtual Velo IdveloNavigation { get; set; } = null!;

    public virtual ICollection<Marquagevelo> Marquagevelos { get; set; } = new List<Marquagevelo>();
}

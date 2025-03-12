using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Ajouteraccessoire
{
    public int Idaccessoire { get; set; }

    public int Idpanier { get; set; }

    public decimal Quantiteaccessoire { get; set; }

    public virtual Accessoire IdaccessoireNavigation { get; set; } = null!;

    public virtual Panier IdpanierNavigation { get; set; } = null!;
}

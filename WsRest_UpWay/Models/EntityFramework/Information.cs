using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class ReInformation
{
    public int Idinformations { get; set; }

    public string? Idreduction { get; set; }

    public int? Idretraitmagasin { get; set; }

    public int Idadresseexp { get; set; }

    public int Idpanier { get; set; }

    public string? Contactinformations { get; set; }

    public bool? Offreemail { get; set; }

    public string? Modelivraison { get; set; }

    public string? Informationpays { get; set; }

    public string? Informationrue { get; set; }

    public virtual Adresseexpedition IdadresseexpNavigation { get; set; } = null!;

    public virtual Panier IdpanierNavigation { get; set; } = null!;

    public virtual Codereduction? IdreductionNavigation { get; set; }

    public virtual Retraitmagasin? IdretraitmagasinNavigation { get; set; }

    public virtual ICollection<Retraitmagasin> Retraitmagasins { get; set; } = new List<Retraitmagasin>();
}

using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Adresseexpedition
{
    public int Idadresseexp { get; set; }

    public int Idclient { get; set; }

    public int? Idadressefact { get; set; }

    public string? Paysexpe { get; set; }

    public string? Batimentexpeoption { get; set; }

    public string? Rueexpe { get; set; }

    public string? Cpexpe { get; set; }

    public string? Regionexpe { get; set; }

    public string? Villeexpe { get; set; }

    public string? Telephoneexpe { get; set; }

    public bool? Donneessauvegardees { get; set; }

    public virtual ICollection<Adressefacturation> Adressefacturations { get; set; } = new List<Adressefacturation>();

    public virtual Adressefacturation? IdadressefactNavigation { get; set; }

    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}

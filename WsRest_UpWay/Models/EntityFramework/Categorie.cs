using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Categorie
{
    public int Idcategorie { get; set; }

    public string? Libellecategorie { get; set; }

    public virtual ICollection<Accessoire> Accessoires { get; set; } = new List<Accessoire>();

    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();

    public virtual ICollection<Caracteristique> Idcaracteristiques { get; set; } = new List<Caracteristique>();
}

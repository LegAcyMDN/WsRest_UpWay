using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Caracteristique
{
    public int Idcaracteristique { get; set; }

    public string? Libellecaracteristique { get; set; }

    public string? Imagecaracteristique { get; set; }

    public virtual ICollection<Caracteristique> CarIdcaracteristiques { get; set; } = new List<Caracteristique>();

    public virtual ICollection<Caracteristique> Idcaracteristiques { get; set; } = new List<Caracteristique>();

    public virtual ICollection<Categorie> Idcategories { get; set; } = new List<Categorie>();

    public virtual ICollection<Velo> Idvelos { get; set; } = new List<Velo>();
}

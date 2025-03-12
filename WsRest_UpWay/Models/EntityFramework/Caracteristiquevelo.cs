using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Caracteristiquevelo
{
    public int Idcaracteristiquevelo { get; set; }

    public decimal Poids { get; set; }

    public int Tubeselle { get; set; }

    public string? Typesuspension { get; set; }

    public string? Couleur { get; set; }

    public string? Typecargo { get; set; }

    public string? Etatbatterie { get; set; }

    public int? Nombrecycle { get; set; }

    public string? Materiau { get; set; }

    public string? Fourche { get; set; }

    public int? Debattement { get; set; }

    public string? Amortisseur { get; set; }

    public int? Debattementamortisseur { get; set; }

    public string? Modeltransmission { get; set; }

    public int? Nombrevitesse { get; set; }

    public string? Freins { get; set; }

    public int? Taillesroues { get; set; }

    public string? Pneus { get; set; }

    public bool? Selletelescopique { get; set; }

    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();


    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}

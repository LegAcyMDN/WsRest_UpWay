using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Keyless]
public partial class Vvelo
{
    [Column("nomvelo")]
    [StringLength(200)]
    public string? Nomvelo { get; set; }

    [Column("anneevelo")]
    [Precision(4, 0)]
    public decimal? Anneevelo { get; set; }

    [Column("taillemin")]
    [StringLength(15)]
    public string? Taillemin { get; set; }

    [Column("taillemax")]
    [StringLength(15)]
    public string? Taillemax { get; set; }

    [Column("nombrekms")]
    [StringLength(15)]
    public string? Nombrekms { get; set; }

    [Column("prixremise")]
    [Precision(5, 0)]
    public decimal? Prixremise { get; set; }

    [Column("prixneuf")]
    [Precision(5, 0)]
    public decimal? Prixneuf { get; set; }

    [Column("pourcentagereduction")]
    [Precision(3, 0)]
    public decimal? Pourcentagereduction { get; set; }

    [Column("descriptifvelo")]
    [StringLength(5000)]
    public string? Descriptifvelo { get; set; }

    [Column("nommarque")]
    [StringLength(100)]
    public string? Nommarque { get; set; }

    [Column("libellecategorie")]
    [StringLength(100)]
    public string? Libellecategorie { get; set; }

    [Column("libellemention")]
    [StringLength(50)]
    public string? Libellemention { get; set; }
}

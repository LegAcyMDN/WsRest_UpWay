using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Keyless]
public partial class Vventenombrevelo
{
    [Column("idvelo")]
    public int? Idvelo { get; set; }

    [Column("nomvelo")]
    [StringLength(200)]
    public string? Nomvelo { get; set; }

    [Column("libellecategorie")]
    [StringLength(100)]
    public string? Libellecategorie { get; set; }

    [Column("nommarque")]
    [StringLength(100)]
    public string? Nommarque { get; set; }

    [Column("taillemin")]
    [StringLength(15)]
    public string? Taillemin { get; set; }

    [Column("taillemax")]
    [StringLength(15)]
    public string? Taillemax { get; set; }

    [Column("modelemoteur")]
    [StringLength(50)]
    public string? Modelemoteur { get; set; }

    [Column("prixpanier")]
    [Precision(11, 2)]
    public decimal? Prixpanier { get; set; }

    [Column("titreassurance")]
    [StringLength(50)]
    public string? Titreassurance { get; set; }

    [Column("dateachat")]
    public DateOnly? Dateachat { get; set; }

    [Column("idadressefact")]
    public int? Idadressefact { get; set; }
}

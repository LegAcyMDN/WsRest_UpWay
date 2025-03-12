using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Keyless]
public partial class Vventenombreacessoire
{
    [Column("idaccessoire")]
    public int? Idaccessoire { get; set; }

    [Column("nomaccessoire")]
    [StringLength(100)]
    public string? Nomaccessoire { get; set; }

    [Column("libellecategorie")]
    [StringLength(100)]
    public string? Libellecategorie { get; set; }

    [Column("prixpanier")]
    [Precision(11, 2)]
    public decimal? Prixpanier { get; set; }

    [Column("dateachat")]
    public DateOnly? Dateachat { get; set; }

    [Column("nommarque")]
    [StringLength(100)]
    public string? Nommarque { get; set; }

    [Column("idadressefact")]
    public int? Idadressefact { get; set; }
}

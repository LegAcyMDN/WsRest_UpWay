using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Keyless]
public partial class Vadresse
{
    [Column("idadressefact")]
    public int? Idadressefact { get; set; }

    [Column("paysfact")]
    [StringLength(50)]
    public string? Paysfact { get; set; }

    [Column("regionfact")]
    [StringLength(20)]
    public string? Regionfact { get; set; }

    [Column("villefact")]
    [StringLength(100)]
    public string? Villefact { get; set; }
}

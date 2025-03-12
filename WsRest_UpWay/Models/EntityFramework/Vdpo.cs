using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Keyless]
public partial class Vdpo
{
    [Column("iddpo")]
    public int? Iddpo { get; set; }

    [Column("idclient")]
    public int? Idclient { get; set; }

    [Column("loginclient")]
    [StringLength(20)]
    public string? Loginclient { get; set; }

    [Column("typeoperation")]
    [StringLength(20)]
    public string? Typeoperation { get; set; }

    [Column("daterequetedpo")]
    public DateOnly? Daterequetedpo { get; set; }
}

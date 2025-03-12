using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Keyless]
public partial class Vcommande
{
    [Column("idvelo")]
    public int? Idvelo { get; set; }

    [Column("titreassurance")]
    [StringLength(50)]
    public string? Titreassurance { get; set; }

    [Column("idpanier")]
    public int? Idpanier { get; set; }

    [Column("idcommande")]
    public int? Idcommande { get; set; }

    [Column("prixpanier")]
    [Precision(11, 2)]
    public decimal? Prixpanier { get; set; }

    [Column("idclient")]
    public int? Idclient { get; set; }
}

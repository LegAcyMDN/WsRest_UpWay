using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("marquagevelo", Schema = "upways")]
[Index("Codemarquage", Name = "codemarquage_unq", IsUnique = true)]
[Index("Idpanier", "Idvelo", Name = "idx_marquagevelo_idpanier_idvelo")]
public partial class Marquagevelo
{
    [Key]
    [Column("codemarquage")]
    [StringLength(10)]
    public string Codemarquage { get; set; } = null!;

    [Column("idpanier")]
    public int Idpanier { get; set; }

    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("prixmarquage")]
    [Precision(4, 2)]
    public decimal? Prixmarquage { get; set; }

    [ForeignKey("Idpanier, Idvelo")]
    [InverseProperty("Marquagevelos")]
    public virtual Lignepanier Lignepanier { get; set; } = null!;
}

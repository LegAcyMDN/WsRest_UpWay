using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_marquage_velo_mal", Schema = "upways")]
[Index("Codemarquage", Name = "codemarquage_unq", IsUnique = true)]
[Index("Idpanier", "Idvelo", Name = "idx_marquagevelo_idpanier_idvelo")]
public partial class Marquagevelo
{
    [Key]
    [Column("mal_code")]
    [StringLength(10)]
    public string Codemarquage { get; set; } = null!;

    [Column("pan_id")]
    public int Idpanier { get; set; }

    [Column("vel_id")]
    public int Idvelo { get; set; }

    [Column("mal_prix", TypeName = "numeric(2, 2)")]
    [Precision(2, 2)]
    public decimal? Prixmarquage { get; set; }

    [ForeignKey("Idpanier, Idvelo")]
    [InverseProperty("Marquagevelos")]
    public virtual Lignepanier Lignepanier { get; set; } = null!;
}

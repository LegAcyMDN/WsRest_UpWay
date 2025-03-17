using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_marquage_velo_mal", Schema = "upways")]
[Index(nameof(CodeMarquage), Name = "codemarquage_unq", IsUnique = true)]
[Index(nameof(PanierId), nameof(VeloId), Name = "ix_t_e_marquagevelo_idpanier_idvelo")]
public partial class Marquagevelo
{
    [Key]
    [Column("mal_code")]
    [StringLength(10)]
    public string CodeMarquage { get; set; } = null!;

    [Column("pan_id")]
    public int PanierId { get; set; }

    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("mal_prix", TypeName = "numeric(2, 2)")]
    [Precision(2, 2)]
    public decimal? PrixMarquage { get; set; }

    [ForeignKey(nameof(VeloId) + "," + nameof(PanierId))]
    [InverseProperty(nameof(Lignepanier.ListeMarquageVelos))]
    public virtual Lignepanier MarquageVeloLignePanier { get; set; } = null!;
}

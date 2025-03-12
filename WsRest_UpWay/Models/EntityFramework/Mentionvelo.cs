using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_mentionvelo_mev", Schema = "upways")]
[Index("Idvelo", Name = "idx_mentionvelo_idvelo")]
public partial class Mentionvelo
{
    [Key]
    [Column("mev_id")]
    public int MentionId { get; set; }

    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("mev_libelle")]
    [StringLength(50)]
    public string? LibelleMention { get; set; }

    [Column("mev_valeur", TypeName = "text")]
    [StringLength(4096)]
    public string? ValeurMention { get; set; }

    [ForeignKey("Idvelo")]
    [InverseProperty("Mentionvelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

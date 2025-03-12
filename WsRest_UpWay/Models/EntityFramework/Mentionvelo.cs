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
    public int Idmention { get; set; }

    [Column("vel_id")]
    public int Idvelo { get; set; }

    [Column("mev_libelle")]
    [StringLength(50)]
    public string? Libellemention { get; set; }

    [Column("mev_valeur", TypeName = "text")]
    [StringLength(4096)]
    public string? Valeurmention { get; set; }

    [ForeignKey("Idvelo")]
    [InverseProperty("Mentionvelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

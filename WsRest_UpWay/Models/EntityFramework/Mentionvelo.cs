using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("mentionvelo", Schema = "upways")]
[Index("Idvelo", Name = "idx_mentionvelo_idvelo")]
public partial class Mentionvelo
{
    [Key]
    [Column("idmention")]
    public int Idmention { get; set; }

    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("libellemention")]
    [StringLength(50)]
    public string? Libellemention { get; set; }

    [Column("valeurmention")]
    [StringLength(4096)]
    public string? Valeurmention { get; set; }

    [ForeignKey("Idvelo")]
    [InverseProperty("Mentionvelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

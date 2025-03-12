using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_testvelo_tev", Schema = "upways")]
[Index("Idmagasin", Name = "idx_testvelo_idmagasin")]
[Index("Idvelo", Name = "idx_testvelo_idvelo")]
public partial class Testvelo
{
    [Key]
    [Column("tev_id")]
    public int Idtest { get; set; }

    [Column("vel_id")]
    public int Idvelo { get; set; }

    [Column("mag_id")]
    public int Idmagasin { get; set; }

    [Column("tev_date")]
    public DateOnly? Datetest { get; set; }

    [Column("tev_heure")]
    public TimeOnly? Heuretest { get; set; }

    [ForeignKey("Idmagasin")]
    [InverseProperty("Testvelos")]
    public virtual Magasin IdmagasinNavigation { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Testvelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

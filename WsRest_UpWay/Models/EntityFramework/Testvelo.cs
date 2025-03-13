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
    public int TestId { get; set; }

    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("mag_id")]
    public int MagasinId { get; set; }

    [Column("tev_date", TypeName = "date")]
    public DateTime? DateTest { get; set; }

    [Column("tev_heure", TypeName = "heure")]
    public TimeOnly? HeureTest { get; set; }

    [ForeignKey("Idmagasin")]
    [InverseProperty("Testvelos")]
    public virtual Magasin TestVeloMagasin { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Testvelos")]
    public virtual Velo TestVeloVelo { get; set; } = null!;
}

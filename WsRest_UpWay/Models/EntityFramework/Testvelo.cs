using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_testvelo_tev", Schema = "upways")]
[Index(nameof(MagasinId), Name = "ix_t_e_testvelo_tev_idmagasin")]
[Index(nameof(VeloId), Name = "ix_t_e_testvelo_tev_idvelo")]
public partial class TestVelo
{
    [Key]
    [Column("tev_id")]
    public int TestId { get; set; }

    [Key]
    [Column("vel_id")]
    public int VeloId { get; set; }

    [Key]
    [Column("mag_id")]
    public int MagasinId { get; set; }

    [Column("tev_date", TypeName = "date")]
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "la date doit être au format français")]
    public DateTime? DateTest { get; set; }

    [Column("tev_heure", TypeName = "heure")]
    public TimeOnly? HeureTest { get; set; }

    [ForeignKey(nameof(MagasinId))]
    [InverseProperty(nameof(Magasin.ListeTestVelos))]
    public virtual Magasin TestVeloMagasin { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeTestVelos))]
    public virtual Velo TestVeloVelo { get; set; } = null!;
}

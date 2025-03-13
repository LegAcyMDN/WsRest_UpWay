using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey(nameof(AlerteId), nameof(ClientId), nameof(VeloId))]
[Table("t_e_alertevelo_alv", Schema = "upways")]
[Index(nameof(ClientId), Name = "ix_t_e_alertevelo_alv_clientid")]
[Index(nameof(VeloId), Name = "ix_t_e_alertevelo_alv_veloid")]
public partial class Alertevelo
{
    [Key]
    [Column("alv_id")]
    public int AlerteId { get; set; }

    [Key]
    [Column("cli_id")]
    public int ClientId { get; set; }

    [Key]
    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("alv_modification", TypeName = "date")]
    public DateTime? ModificationAlerte { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Compteclient.ListeAlerteVelos))]
    public virtual Compteclient AlerteClient { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeAlerteVelos))]
    public virtual Velo AlerteVelo { get; set; } = null!;
}

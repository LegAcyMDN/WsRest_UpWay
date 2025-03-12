using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_alertevelo_alv")]
public partial class Alertevelo
{
    [Key]
    [Column("alv_id")]
    public int AlerteId { get; set; }

    [Column("alv_modification", TypeName = "date")]
    public DateTime? ModificationAlerte { get; set; }

    [Key]
    [Column("cli_id")]
    public int ClientId { get; set; }

    [Key]
    [Column("vel_id")]
    public int VeloId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

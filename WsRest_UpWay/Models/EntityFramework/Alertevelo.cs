using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idalerte", "Idclient", "Idvelo")]
[Table("t_e_alertevelo_alv", Schema = "upways")]
[Index("Idclient", Name = "idx_alerte_idclient")]
[Index("Idvelo", Name = "idx_alerte_idvelo")]
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

    [ForeignKey("Idclient")]
    [InverseProperty("Alertevelos")]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Alertevelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

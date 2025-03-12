using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_photovelo_phv", Schema = "upways")]
[Index("Idvelo", Name = "idx_photovelo_idvelo")]
public partial class Photovelo
{
    [Key]
    [Column("phv_id")]
    public int PhotoVeloId { get; set; }

    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("phv_url")]
    [StringLength(4096)]
    public string? UrlPhotoVelo { get; set; }

    [Column("phv_bytea")]
    public byte[]? PhotoBytea { get; set; }

    [ForeignKey("Idvelo")]
    [InverseProperty("Photovelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

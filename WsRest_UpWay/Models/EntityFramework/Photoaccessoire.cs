using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_photoaccessoire_pha", Schema = "upways")]
[Index("Idaccessoire", Name = "idx_photoaccessoire_idaccessoire")]
public partial class Photoaccessoire
{
    [Key]
    [Column("pha_id")]
    public int PhotoAcessoireId { get; set; }

    [Column("acs_id")]
    public int AccessoireId { get; set; }

    [Column("pha_url")]
    public byte[]? UrlPhotoAccessoire { get; set; }

    [ForeignKey("Idaccessoire")]
    [InverseProperty("Photoaccessoires")]
    public virtual Accessoire PhotoAccessoireAccessoire { get; set; } = null!;
}

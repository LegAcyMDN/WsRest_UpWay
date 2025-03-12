using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("photoaccessoire", Schema = "upways")]
[Index("Idaccessoire", Name = "idx_photoaccessoire_idaccessoire")]
public partial class Photoaccessoire
{
    [Key]
    [Column("idphotoaccessoire")]
    public int Idphotoaccessoire { get; set; }

    [Column("idaccessoire")]
    public int Idaccessoire { get; set; }

    [Column("urlphotoaccessoire")]
    public byte[]? Urlphotoaccessoire { get; set; }

    [ForeignKey("Idaccessoire")]
    [InverseProperty("Photoaccessoires")]
    public virtual Accessoire IdaccessoireNavigation { get; set; } = null!;
}

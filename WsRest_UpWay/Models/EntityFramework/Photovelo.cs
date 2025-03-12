using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("photovelo", Schema = "upways")]
[Index("Idvelo", Name = "idx_photovelo_idvelo")]
public partial class Photovelo
{
    [Key]
    [Column("idphotovelo")]
    public int Idphotovelo { get; set; }

    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("urlphotovelo")]
    [StringLength(4096)]
    public string? Urlphotovelo { get; set; }

    [Column("photobytea")]
    public byte[]? Photobytea { get; set; }

    [ForeignKey("Idvelo")]
    [InverseProperty("Photovelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

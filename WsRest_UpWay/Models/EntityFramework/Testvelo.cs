using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("testvelo", Schema = "upways")]
[Index("Idmagasin", Name = "idx_testvelo_idmagasin")]
[Index("Idvelo", Name = "idx_testvelo_idvelo")]
public partial class Testvelo
{
    [Key]
    [Column("idtest")]
    public int Idtest { get; set; }

    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("idmagasin")]
    public int Idmagasin { get; set; }

    [Column("datetest")]
    public DateOnly? Datetest { get; set; }

    [Column("heuretest")]
    public TimeOnly? Heuretest { get; set; }

    [ForeignKey("Idmagasin")]
    [InverseProperty("Testvelos")]
    public virtual Magasin IdmagasinNavigation { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Testvelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

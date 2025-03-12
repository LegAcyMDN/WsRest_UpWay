using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idutilite", "Idvelo")]
[Table("utilite", Schema = "upways")]
[Index("Idvelo", Name = "idx_utilite_idvelo")]
public partial class Utilite
{
    [Key]
    [Column("idutilite")]
    public int Idutilite { get; set; }

    [Key]
    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("valeurutilite")]
    public decimal? Valeurutilite { get; set; }

    [ForeignKey("Idvelo")]
    [InverseProperty("Utilites")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idalerte", "Idclient", "Idvelo")]
[Table("alertevelo", Schema = "upways")]
[Index("Idclient", Name = "idx_alerte_idclient")]
[Index("Idvelo", Name = "idx_alerte_idvelo")]
public partial class Alertevelo
{
    [Key]
    [Column("idalerte")]
    public int Idalerte { get; set; }

    [Key]
    [Column("idclient")]
    public int Idclient { get; set; }

    [Key]
    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("modifalerte", TypeName = "timestamp without time zone")]
    public DateTime? Modifalerte { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Alertevelos")]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Alertevelos")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

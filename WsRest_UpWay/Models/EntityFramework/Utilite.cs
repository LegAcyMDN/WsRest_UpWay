using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idutilite", "Idvelo")]
[Table("t_j_utilite_uti", Schema = "upways")]
[Index(nameof(VeloId), Name = "idx_utilite_idvelo")]
public partial class Utilite
{
    [Key]
    [Column("uti_id")]
    public int UtiliteId { get; set; }

    [Key]
    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("uti_valeur")]
    public decimal? ValeurUtilite { get; set; }

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeUtilites))]
    public virtual Velo UtiliteVelo { get; set; } = null!;
}

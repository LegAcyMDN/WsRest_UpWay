using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_rapportinspection_ras", Schema = "upways")]
public partial class Rapportinspection
{
    [Key]
    [Column("ras_id")]
    public int InspectionId { get; set; }

    [Column("ras_type")]
    [StringLength(200)]
    public string? TypeInspection { get; set; }

    [Column("ras_sousType")]
    [StringLength(200)]
    public string? SousTypeInspection { get; set; }

    [Column("ras_pointdInspection")]
    [StringLength(200)]
    public string? PointDInspection { get; set; }

    [InverseProperty(nameof(Estrealise.EstRealiseRapportInspection))]
    public virtual ICollection<Estrealise> ListeEstRealises { get; set; } = new List<Estrealise>();
}

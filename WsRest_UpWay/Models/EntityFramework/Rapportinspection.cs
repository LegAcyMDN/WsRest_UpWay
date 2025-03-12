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
    public int Idinspection { get; set; }

    [Column("ras_type")]
    [StringLength(200)]
    public string? TypeInspection { get; set; }

    [Column("ras_sousType")]
    [StringLength(200)]
    public string? SousTypeInspection { get; set; }

    [Column("ras_pointdInspection")]
    [StringLength(200)]
    public string? PointDInspection { get; set; }

    [InverseProperty("IdinspectionNavigation")]
    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();
}

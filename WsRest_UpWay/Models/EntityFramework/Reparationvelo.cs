using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_reparationvelo_rev", Schema = "upways")]
public partial class ReparationVelo
{
    [Key]
    [Column("rev_id")]
    public int ReparationId { get; set; }

    [Column("rev_check")]
    public bool? CheckReparation { get; set; }

    [Column("rev_checkValidation")]
    public bool? CheckValidation { get; set; }

    [InverseProperty(nameof(EstRealise.EstRealiseReparationVelo))]
    public virtual ICollection<EstRealise> ListeEstRealises { get; set; } = new List<EstRealise>();
}

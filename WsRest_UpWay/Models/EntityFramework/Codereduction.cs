using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_codereduction_cor", Schema = "upways")]
public partial class Codereduction
{
    [Key]
    [Column("cor_id")]
    [StringLength(20)]
    public string ReductionId { get; set; } = null!;

    [Column("cor_actifreduction")]
    public bool? ActifReduction { get; set; }

    [Column("cor_reduction")]
    public int? Reduction { get; set; }

    [InverseProperty("IdreductionNavigation")]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();
}

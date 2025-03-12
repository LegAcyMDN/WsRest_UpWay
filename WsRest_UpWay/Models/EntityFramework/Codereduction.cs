using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("codereduction", Schema = "upways")]
public partial class Codereduction
{
    [Key]
    [Column("idreduction")]
    [StringLength(20)]
    public string Idreduction { get; set; } = null!;

    [Column("actifreduction")]
    public bool? Actifreduction { get; set; }

    [Column("reduction")]
    public int? Reduction { get; set; }

    [InverseProperty("IdreductionNavigation")]
    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}

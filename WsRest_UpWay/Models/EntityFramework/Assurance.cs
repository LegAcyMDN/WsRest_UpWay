using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_assurance_ass")]
public partial class Assurance
{
    [Key]
    [Column("ass_id")]
    public int AssuranceId { get; set; }

    [Column("ass_titre")]
    [StringLength(20)]
    public string? TitreAssurance { get; set; }

    [Column("ass_description")]
    [StringLength(100)]
    public string? DescriptionAssurance { get; set; }

    [Column("ass_prix", TypeName = "numeric(2, 2)")]
    public decimal? PrixAssurance { get; set; }

    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}

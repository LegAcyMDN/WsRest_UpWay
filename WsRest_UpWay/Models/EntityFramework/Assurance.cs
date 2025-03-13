using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_assurance_ass", Schema = "upways")]
public partial class Assurance
{
    public Assurance()
    {
        ListeLignePaniers = new HashSet<Lignepanier>();
    }

    [Key]
    [Column("ass_id")]
    public int AssuranceId { get; set; }

    [Column("ass_titre")]
    [StringLength(50)]
    public string? TitreAssurance { get; set; }

    [Column("ass_description", TypeName = "text")]
    [StringLength(4096)]
    public string? DescriptionAssurance { get; set; }

    [Column("ass_prix", TypeName = "numeric(4, 2)")]
    public decimal? PrixAssurance { get; set; }

    [InverseProperty(nameof(Lignepanier.LignePanierAssurance))]
    public virtual ICollection<Lignepanier> ListeLignePaniers { get; set; } = new List<Lignepanier>();
}

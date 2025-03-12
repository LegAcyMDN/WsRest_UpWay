﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_assurance_ass", Schema = "upways")]
public partial class Assurance
{
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

    [InverseProperty("IdassuranceNavigation")]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}

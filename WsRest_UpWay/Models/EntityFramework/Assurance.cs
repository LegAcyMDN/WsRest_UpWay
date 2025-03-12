using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("assurance", Schema = "upways")]
public partial class Assurance
{
    [Key]
    [Column("idassurance")]
    public int Idassurance { get; set; }

    [Column("titreassurance")]
    [StringLength(50)]
    public string? Titreassurance { get; set; }

    [Column("descriptionassurance")]
    [StringLength(4096)]
    public string? Descriptionassurance { get; set; }

    [Column("prixassurance")]
    [Precision(4, 2)]
    public decimal? Prixassurance { get; set; }

    [InverseProperty("IdassuranceNavigation")]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}

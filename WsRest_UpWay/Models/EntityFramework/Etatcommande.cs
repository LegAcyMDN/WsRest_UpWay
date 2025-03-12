using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("etatcommande", Schema = "upways")]
public partial class Etatcommande
{
    [Key]
    [Column("idetatcommande")]
    public int Idetatcommande { get; set; }

    [Column("libelleetat")]
    [StringLength(20)]
    public string? Libelleetat { get; set; }

    [InverseProperty("IdetatcommandeNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();
}

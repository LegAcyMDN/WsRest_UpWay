using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_etatcommande_etc", Schema = "upways")]
public partial class Etatcommande
{
    [Key]
    [Column("etc_id")]
    public int EtatCommandeId { get; set; }

    [Column("etc_libelle")]
    [StringLength(20)]
    public string? LibelleEtat { get; set; }

    [InverseProperty("IdetatcommandeNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();
}

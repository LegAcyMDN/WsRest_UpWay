using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_etatcommande_etc", Schema = "upways")]
public partial class EtatCommande
{
    public EtatCommande()
    {
        ListeDetailCommandes = new HashSet<DetailCommande>();
    }
    [Key]
    [Column("etc_id")]
    public int EtatCommandeId { get; set; }

    [Column("etc_libelle")]
    [StringLength(20)]
    public string? LibelleEtat { get; set; }

    [InverseProperty(nameof(DetailCommande.DetailCommandeEtat))]
    public virtual ICollection<DetailCommande> ListeDetailCommandes { get; set; } = new List<DetailCommande>();
}

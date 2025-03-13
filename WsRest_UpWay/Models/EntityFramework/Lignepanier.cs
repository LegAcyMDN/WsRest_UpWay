using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idpanier", "Idvelo")]
[Table("t_e_lignepanier_lignpan", Schema = "upways")]
[Index("Idassurance", Name = "idx_linepanier_idassurance")]
[Index("Idpanier", Name = "idx_linepanier_idpanier")]
[Index("Idvelo", Name = "idx_linepanier_idvelo")]
public partial class Lignepanier
{
    public Lignepanier()
    {
        ListeMarquageVelos = new HashSet<Marquagevelo>();
    }

    [Key]
    [Column("lignpan_id")]
    public int PanierId { get; set; }

    [Key]
    [Column("vel_id")]
    public int VeloId { get; set; }

    [Column("ass_id")]
    public int AssuranceId { get; set; }

    [Column("lignpan_quantpan")]
    [Precision(2, 0)]
    public decimal? QuantitePanier { get; set; }

    [Column("lignpan_priquant")]
    [Precision(11, 2)]
    public decimal? PrixQuantite { get; set; }

    [ForeignKey(nameof(AssuranceId))]
    [InverseProperty(nameof(Assurance.ListeLignePaniers))]
    public virtual Assurance LignePanierAssurance { get; set; } = null!;

    [ForeignKey(nameof(PanierId))]
    [InverseProperty(nameof(Panier.ListeLignePaniers))]
    public virtual Panier LignePanierPanier { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeLignePaniers))]
    public virtual Velo LignePanierVelo { get; set; } = null!;

    [InverseProperty(nameof(Marquagevelo.MarquageVeloLignePanier))]
    public virtual ICollection<Marquagevelo> ListeMarquageVelos { get; set; } = new List<Marquagevelo>();
}

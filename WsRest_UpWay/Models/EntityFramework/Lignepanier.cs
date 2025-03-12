using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idpanier", "Idvelo")]
[Table("t_e_lignepanier_lignpan", Schema = "upways")]
[Index("Idassurance", Name = "idx_linepanier_idassurance")]
[Index("Idpanier", Name = "idx_linepanier_idpanier")]
[Index("Idvelo", Name = "idx_linepanier_idvelo")]
public partial class Lignepanier
{
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

    [ForeignKey("Idassurance")]
    [InverseProperty("Lignepaniers")]
    public virtual Assurance IdassuranceNavigation { get; set; } = null!;

    [ForeignKey("Idpanier")]
    [InverseProperty("Lignepaniers")]
    public virtual Panier IdpanierNavigation { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Lignepaniers")]
    public virtual Velo IdveloNavigation { get; set; } = null!;

    [InverseProperty("Lignepanier")]
    public virtual ICollection<Marquagevelo> Marquagevelos { get; set; } = new List<Marquagevelo>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idpanier", "Idvelo")]
[Table("lignepanier", Schema = "upways")]
[Index("Idassurance", Name = "idx_linepanier_idassurance")]
[Index("Idpanier", Name = "idx_linepanier_idpanier")]
[Index("Idvelo", Name = "idx_linepanier_idvelo")]
public partial class Lignepanier
{
    [Key]
    [Column("idpanier")]
    public int Idpanier { get; set; }

    [Key]
    [Column("idvelo")]
    public int Idvelo { get; set; }

    [Column("idassurance")]
    public int Idassurance { get; set; }

    [Column("quantitepanier")]
    [Precision(2, 0)]
    public decimal? Quantitepanier { get; set; }

    [Column("prixquantite")]
    [Precision(11, 2)]
    public decimal? Prixquantite { get; set; }

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

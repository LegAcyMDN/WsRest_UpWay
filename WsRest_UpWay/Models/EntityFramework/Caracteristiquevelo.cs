using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("caracteristiquevelo", Schema = "upways")]
public partial class Caracteristiquevelo
{
    [Key]
    [Column("idcaracteristiquevelo")]
    public int Idcaracteristiquevelo { get; set; }

    [Column("poids")]
    [Precision(5, 2)]
    public decimal Poids { get; set; }

    [Column("tubeselle")]
    public int Tubeselle { get; set; }

    [Column("typesuspension")]
    [StringLength(20)]
    public string? Typesuspension { get; set; }

    [Column("couleur")]
    [StringLength(10)]
    public string? Couleur { get; set; }

    [Column("typecargo")]
    [StringLength(20)]
    public string? Typecargo { get; set; }

    [Column("etatbatterie")]
    [StringLength(10)]
    public string? Etatbatterie { get; set; }

    [Column("nombrecycle")]
    public int? Nombrecycle { get; set; }

    [Column("materiau")]
    [StringLength(20)]
    public string? Materiau { get; set; }

    [Column("fourche")]
    [StringLength(50)]
    public string? Fourche { get; set; }

    [Column("debattement")]
    public int? Debattement { get; set; }

    [Column("amortisseur")]
    [StringLength(50)]
    public string? Amortisseur { get; set; }

    [Column("debattementamortisseur")]
    public int? Debattementamortisseur { get; set; }

    [Column("modeltransmission")]
    [StringLength(50)]
    public string? Modeltransmission { get; set; }

    [Column("nombrevitesse")]
    public int? Nombrevitesse { get; set; }

    [Column("freins")]
    [StringLength(30)]
    public string? Freins { get; set; }

    [Column("taillesroues")]
    public int? Taillesroues { get; set; }

    [Column("pneus")]
    [StringLength(100)]
    public string? Pneus { get; set; }

    [Column("selletelescopique")]
    public bool? Selletelescopique { get; set; }

    [InverseProperty("IdcaracteristiqueveloNavigation")]
    public virtual ICollection<Velomodifier> Velomodifiers { get; set; } = new List<Velomodifier>();

    [InverseProperty("IdcaracteristiqueveloNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}

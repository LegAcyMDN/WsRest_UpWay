using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_adressefacturation_adf")]
public partial class Adressefacturation
{
    [Key]
    [Column("adf_id")]
    public int AdresseFactId { get; set; }

    [Column("adf_pays")]
    [StringLength(50)]
    public string? PaysFacturation { get; set; }

    [Column("adf_batopt")]
    [StringLength(100)]
    public string? BatimentFacturationOpt { get; set; }

    [Column("adf_rue")]
    [StringLength(200)]
    public string? RueFacturation { get; set; }

    [Column("adf_cp", TypeName = "char(5)")]
    [StringLength(5)]
    public string? CPFacturation { get; set; }

    [Column("adf_region")]
    [StringLength(100)]
    public string? Regionfacturation { get; set; }

    [Column("adf_ville")]
    [StringLength(20)]
    public string? VilleFacturation { get; set; }

    [Column("adf_telephone")]
    [StringLength(10)]
    public string? TelephoneFacturation { get; set; }

    [Key]
    [Column("cli_id")]
    public int ClientId { get; set; }

    [Key]
    [Column("ade_id")]
    public int? AdresseExpeId { get; set; }

    [ForeignKey(nameof(AdresseExpeId))]
    public virtual Adresseexpedition? IdadresseexpNavigation { get; set; }

    [ForeignKey(nameof(ClientId))]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    public virtual ICollection<Adresseexpedition> Adresseexpeditions { get; set; } = new List<Adresseexpedition>();

    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();
}

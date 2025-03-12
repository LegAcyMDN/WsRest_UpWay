using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_adresseexpedition_ade")]
public partial class Adresseexpedition
{
    [Key]
    [Column("ade_id")]
    public int AdresseExpeId { get; set; }

    [Column("ade_pays")]
    [StringLength(50)]
    public string? PaysExpedition { get; set; }

    [Column("ade_batopt")]
    [StringLength(100)]
    public string? BatimentExpeditionOption { get; set; }

    [Column("ade_rue")]
    [StringLength(200)]
    public string? RueExpedition { get; set; }

    [Column("ade_cp")]
    [StringLength(5)]
    public string? CPExpedition { get; set; }

    [Column("ade_region")]
    [StringLength(100)]
    public string? RegionExpedition { get; set; }

    [Column("ade_ville")]
    [StringLength(20)]
    public string? VilleExpedition { get; set; }

    [Column("ade_telephone")]
    [StringLength(10)]
    public string? TelephoneExpedition { get; set; }

    [Column("ade_donneessauv")]
    public bool? DonneesSauvegardees { get; set; }

    [Key]
    [Column("cli_id")]
    public int ClientId { get; set; }

    [Key]
    [Column("adf_id")]
    public int? AdresseFactId { get; set; }

    [ForeignKey(nameof(AdresseFactId))]
    public virtual Adressefacturation? IdadressefactNavigation { get; set; }

    [ForeignKey(nameof(ClientId))]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    public virtual ICollection<Adressefacturation> Adressefacturations { get; set; } = new List<Adressefacturation>();

    public virtual ICollection<ReInformation> Information { get; set; } = new List<ReInformation>();
}

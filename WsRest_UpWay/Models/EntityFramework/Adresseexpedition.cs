using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_adresseexpedition_ade", Schema = "upways")]
[Index("Idadressefact", Name = "idx_adresseexp_idadressefact")]
[Index("Idclient", Name = "idx_adresseexp_idclient")]
public partial class Adresseexpedition
{
    [Key]
    [Column("ade_id")]
    public int AdresseExpeId { get; set; }

    [Column("cli_id")]
    public int ClientId { get; set; }

    [Column("adf_id")]
    public int? AdresseFactId { get; set; }

    [Column("ade_pays")]
    [StringLength(50)]
    public string? PaysExpedition { get; set; }

    [Column("ade_batopt")]
    [StringLength(100)]
    public string? BatimentExpeditionOpt { get; set; }

    [Column("ade_rue")]
    [StringLength(200)]
    public string? RueExpedition { get; set; }

    [Column("ade_cp", TypeName = "char(5)")]
    [StringLength(5)]
    public string? CPExpedition { get; set; }

    [Column("ade_region")]
    [StringLength(20)]
    public string? RegionExpedition { get; set; }

    [Column("ade_ville")]
    [StringLength(100)]
    public string? VilleExpedition { get; set; }

    [Column("ade_telephone", TypeName = "char(10)")]
    [StringLength(10)]
    public string? TelephoneExpedition { get; set; }

    [Column("ade_donneessauv")]
    public bool? DonneesSauvegardees { get; set; }

    [ForeignKey("Idadressefact")]
    [InverseProperty("Adresseexpeditions")]
    public virtual Adressefacturation? AdresseExpeFact { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Adresseexpeditions")]
    public virtual Compteclient AdresseExpeClient { get; set; } = null!;

    [InverseProperty("IdadresseexpNavigation")]
    public virtual ICollection<Adressefacturation> ListeAdresseFact { get; set; } = new List<Adressefacturation>();

    [InverseProperty("IdadresseexpNavigation")]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_adresseexpedition_ade", Schema = "upways")]
[Index(nameof(AdresseFactId), Name = "ix_t_e_adresseexpedition_ade_adressefactid")]
[Index(nameof(ClientId), Name = "ix_t_e_adresseexpedition_ade_clientid")]
public partial class Adresseexpedition
{
    public Adresseexpedition()
    {
        ListeAdresseFact = new HashSet<Adressefacturation>();
        ListeInformations = new HashSet<Information>();
    }

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

    [ForeignKey(nameof(AdresseFactId))]
    [InverseProperty(nameof(Adressefacturation.ListeAdresseExpe))]
    public virtual Adressefacturation? AdresseExpeFact { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Compteclient.ListeAdresseExpe))]
    public virtual Compteclient AdresseExpeClient { get; set; } = null!;

    [InverseProperty(nameof(Adressefacturation.AdresseFactExpe))]
    public virtual ICollection<Adressefacturation> ListeAdresseFact { get; set; } = new List<Adressefacturation>();

    [InverseProperty(nameof(Information.InformationAdresseExpe))]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();
}

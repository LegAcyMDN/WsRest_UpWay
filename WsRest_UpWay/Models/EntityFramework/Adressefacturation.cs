using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_adressefacturation_adf", Schema = "upways")]
[Index(nameof(AdresseExpId), Name = "ix_t_e_adressefacturation_adf_adresseexpeid")]
[Index(nameof(ClientId), Name = "ix_t_e_adressefacturation_adf_clientid")]
public partial class Adressefacturation
{
    public Adressefacturation()
    {
        ListeAdresseExpe = new HashSet<Adresseexpedition>();
        ListeDetailCommande = new HashSet<Detailcommande>();
    }

    [Key]
    [Column("adf_id")]
    public int AdresseFactId { get; set; }

    [Column("cli_id")]
    public int ClientId { get; set; }

    [Column("ade_id")]
    public int? AdresseExpId { get; set; }

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
    [StringLength(20)]
    public string? RegionFacturation { get; set; }

    [Column("adf_ville")]
    [StringLength(100)]
    public string? VilleFacturation { get; set; }

    [Column("adf_telephone", TypeName = "char(10)")]
    [StringLength(10)]
    public string? TelephoneFacturation { get; set; }

    [ForeignKey(nameof(AdresseExpId))]
    [InverseProperty(nameof(Adresseexpedition.ListeAdresseFact))]
    public virtual Adresseexpedition? AdresseFactExpe { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty("Adressefacturations")]
    public virtual Compteclient AdresseFactClient { get; set; } = null!;

    [InverseProperty(nameof(Adresseexpedition.AdresseExpeFact))]
    public virtual ICollection<Adresseexpedition> ListeAdresseExpe { get; set; } = new List<Adresseexpedition>();

    [InverseProperty("IdadressefactNavigation")]
    public virtual ICollection<Detailcommande> ListeDetailCommande { get; set; } = new List<Detailcommande>();
}

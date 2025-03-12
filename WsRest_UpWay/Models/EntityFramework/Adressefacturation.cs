using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("adressefacturation", Schema = "upways")]
[Index("Idadresseexp", Name = "idx_adressefact_idadresseexp")]
[Index("Idclient", Name = "idx_adressefact_idclient")]
public partial class Adressefacturation
{
    [Key]
    [Column("idadressefact")]
    public int Idadressefact { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idadresseexp")]
    public int? Idadresseexp { get; set; }

    [Column("paysfact")]
    [StringLength(50)]
    public string? Paysfact { get; set; }

    [Column("appartementfact")]
    [StringLength(100)]
    public string? Appartementfact { get; set; }

    [Column("ruefact")]
    [StringLength(200)]
    public string? Ruefact { get; set; }

    [Column("cpfact")]
    [StringLength(10)]
    public string? Cpfact { get; set; }

    [Column("regionfact")]
    [StringLength(20)]
    public string? Regionfact { get; set; }

    [Column("villefact")]
    [StringLength(100)]
    public string? Villefact { get; set; }

    [Column("telephonefact")]
    [StringLength(14)]
    public string? Telephonefact { get; set; }

    [InverseProperty("IdadressefactNavigation")]
    public virtual ICollection<Adresseexpedition> Adresseexpeditions { get; set; } = new List<Adresseexpedition>();

    [InverseProperty("IdadressefactNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [ForeignKey("Idadresseexp")]
    [InverseProperty("Adressefacturations")]
    public virtual Adresseexpedition? IdadresseexpNavigation { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Adressefacturations")]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;
}

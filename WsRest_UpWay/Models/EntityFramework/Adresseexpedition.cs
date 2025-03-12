using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("adresseexpedition", Schema = "upways")]
[Index("Idadressefact", Name = "idx_adresseexp_idadressefact")]
[Index("Idclient", Name = "idx_adresseexp_idclient")]
public partial class Adresseexpedition
{
    [Key]
    [Column("idadresseexp")]
    public int Idadresseexp { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idadressefact")]
    public int? Idadressefact { get; set; }

    [Column("paysexpe")]
    [StringLength(50)]
    public string? Paysexpe { get; set; }

    [Column("batimentexpeoption")]
    [StringLength(100)]
    public string? Batimentexpeoption { get; set; }

    [Column("rueexpe")]
    [StringLength(200)]
    public string? Rueexpe { get; set; }

    [Column("cpexpe")]
    [StringLength(10)]
    public string? Cpexpe { get; set; }

    [Column("regionexpe")]
    [StringLength(20)]
    public string? Regionexpe { get; set; }

    [Column("villeexpe")]
    [StringLength(100)]
    public string? Villeexpe { get; set; }

    [Column("telephoneexpe")]
    [StringLength(14)]
    public string? Telephoneexpe { get; set; }

    [Column("donneessauvegardees")]
    public bool? Donneessauvegardees { get; set; }

    [InverseProperty("IdadresseexpNavigation")]
    public virtual ICollection<Adressefacturation> Adressefacturations { get; set; } = new List<Adressefacturation>();

    [ForeignKey("Idadressefact")]
    [InverseProperty("Adresseexpeditions")]
    public virtual Adressefacturation? IdadressefactNavigation { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Adresseexpeditions")]
    public virtual Compteclient IdclientNavigation { get; set; } = null!;

    [InverseProperty("IdadresseexpNavigation")]
    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}

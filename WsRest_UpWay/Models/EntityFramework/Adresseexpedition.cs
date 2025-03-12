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
    public int Idadresseexp { get; set; }

    [Column("cli_id")]
    public int Idclient { get; set; }

    [Column("adf_id")]
    public int? Idadressefact { get; set; }

    [Column("ade_pays")]
    [StringLength(50)]
    public string? Paysexpe { get; set; }

    [Column("ade_batopt")]
    [StringLength(100)]
    public string? Batimentexpeoption { get; set; }

    [Column("ade_rue")]
    [StringLength(200)]
    public string? Rueexpe { get; set; }

    [Column("ade_cp", TypeName = "char(5)")]
    [StringLength(5)]
    public string? Cpexpe { get; set; }

    [Column("ade_region")]
    [StringLength(20)]
    public string? Regionexpe { get; set; }

    [Column("ade_ville")]
    [StringLength(100)]
    public string? Villeexpe { get; set; }

    [Column("ade_telephone", TypeName = "char(10)")]
    [StringLength(10)]
    public string? Telephoneexpe { get; set; }

    [Column("ade_donneessauv")]
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

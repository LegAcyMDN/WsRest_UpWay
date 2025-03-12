using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("informations", Schema = "upways")]
[Index("Idadresseexp", Name = "idx_informations_idadresseexp")]
[Index("Idpanier", Name = "idx_informations_idpanier")]
[Index("Idreduction", Name = "idx_informations_idreduction")]
[Index("Idretraitmagasin", Name = "idx_informations_idretraitmagasin")]
public partial class Information
{
    [Key]
    [Column("idinformations")]
    public int Idinformations { get; set; }

    [Column("idreduction")]
    [StringLength(20)]
    public string? Idreduction { get; set; }

    [Column("idretraitmagasin")]
    public int? Idretraitmagasin { get; set; }

    [Column("idadresseexp")]
    public int Idadresseexp { get; set; }

    [Column("idpanier")]
    public int Idpanier { get; set; }

    [Column("contactinformations")]
    [StringLength(100)]
    public string? Contactinformations { get; set; }

    [Column("offreemail")]
    public bool? Offreemail { get; set; }

    [Column("modelivraison")]
    [StringLength(30)]
    public string? Modelivraison { get; set; }

    [Column("informationpays")]
    [StringLength(20)]
    public string? Informationpays { get; set; }

    [Column("informationrue")]
    [StringLength(200)]
    public string? Informationrue { get; set; }

    [ForeignKey("Idadresseexp")]
    [InverseProperty("Information")]
    public virtual Adresseexpedition IdadresseexpNavigation { get; set; } = null!;

    [ForeignKey("Idpanier")]
    [InverseProperty("Information")]
    public virtual Panier IdpanierNavigation { get; set; } = null!;

    [ForeignKey("Idreduction")]
    [InverseProperty("Information")]
    public virtual Codereduction? IdreductionNavigation { get; set; }

    [ForeignKey("Idretraitmagasin")]
    [InverseProperty("Information")]
    public virtual Retraitmagasin? IdretraitmagasinNavigation { get; set; }

    [InverseProperty("IdinformationsNavigation")]
    public virtual ICollection<Retraitmagasin> Retraitmagasins { get; set; } = new List<Retraitmagasin>();
}

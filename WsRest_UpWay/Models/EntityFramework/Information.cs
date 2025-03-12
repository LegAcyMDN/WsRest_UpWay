using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_informations_inf", Schema = "upways")]
[Index("Idadresseexp", Name = "idx_informations_idadresseexp")]
[Index("Idpanier", Name = "idx_informations_idpanier")]
[Index("Idreduction", Name = "idx_informations_idreduction")]
[Index("Idretraitmagasin", Name = "idx_informations_idretraitmagasin")]
public partial class Information
{
    [Key]
    [Column("inf_id")]
    public int Idinformations { get; set; }

    [Column("red_id")]
    [StringLength(20)]
    public string? Idreduction { get; set; }

    [Column("retmag_id")]
    public int? Idretraitmagasin { get; set; }

    [Column("adexp_id")]
    public int Idadresseexp { get; set; }

    [Column("pan_id")]
    public int Idpanier { get; set; }

    [Column("inf_continf")]
    [StringLength(100)]
    public string? Contactinformations { get; set; }

    [Column("inf_offmail")]
    public bool? Offreemail { get; set; }

    [Column("inf_moodliv")]
    [StringLength(30)]
    public string? Modelivraison { get; set; }

    [Column("inf_payinf")]
    [StringLength(20)]
    public string? Informationpays { get; set; }

    [Column("inf_rueinf")]
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

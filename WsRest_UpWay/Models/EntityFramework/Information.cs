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
    public int InformationId { get; set; }

    [Column("red_id")]
    [StringLength(20)]
    public string? ReductionId { get; set; }

    [Column("retmag_id")]
    public int? RetraitMagasinId { get; set; }

    [Column("adexp_id")]
    public int AdresseExpeId { get; set; }

    [Column("pan_id")]
    public int PanierId { get; set; }

    [Column("inf_continf")]
    [StringLength(100)]
    public string? ContactInformations { get; set; }

    [Column("inf_offmail")]
    public bool? OffreEmail { get; set; }

    [Column("inf_moodliv")]
    [StringLength(30)]
    public string? ModeLivraison { get; set; }

    [Column("inf_payinf")]
    [StringLength(20)]
    public string? InformationPays { get; set; }

    [Column("inf_rueinf")]
    [StringLength(200)]
    public string? InformationRue { get; set; }

    [ForeignKey("Idadresseexp")]
    [InverseProperty("Information")]
    public virtual Adresseexpedition InformationAdresseExpe { get; set; } = null!;

    [ForeignKey("Idpanier")]
    [InverseProperty("Information")]
    public virtual Panier InformationPanier { get; set; } = null!;

    [ForeignKey("Idreduction")]
    [InverseProperty("Information")]
    public virtual Codereduction? InformationCodeReduction { get; set; }

    [ForeignKey("Idretraitmagasin")]
    [InverseProperty("Information")]
    public virtual Retraitmagasin? InformationRetraitMagasin { get; set; }

    [InverseProperty("IdinformationsNavigation")]
    public virtual ICollection<Retraitmagasin> ListeRetraitMagasins { get; set; } = new List<Retraitmagasin>();
}

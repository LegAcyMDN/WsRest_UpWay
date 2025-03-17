using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_informations_inf", Schema = "upways")]
[Index(nameof(AdresseExpeId), Name = "ix_t_e_informations_inf_adresseexpeid")]
[Index(nameof(PanierId), Name = "ix_t_e_informations_inf_panierid")]
[Index(nameof(ReductionId), Name = "ix_t_e_informations_inf_reductionid")]
[Index(nameof(RetraitMagasinId), Name = "ix_t_e_informations_inf_retraitmagasinid")]
public partial class Information
{
    public Information()
    {
        ListeRetraitMagasins = new HashSet<RetraitMagasin>();
    }

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

    [ForeignKey(nameof(AdresseExpeId))]
    [InverseProperty(nameof(AdresseExpedition.ListeInformations))]
    public virtual AdresseExpedition InformationAdresseExpe { get; set; } = null!;

    [ForeignKey(nameof(PanierId))]
    [InverseProperty(nameof(Panier.ListeInformations))]
    public virtual Panier InformationPanier { get; set; } = null!;

    [ForeignKey(nameof(ReductionId))]
    [InverseProperty(nameof(CodeReduction.ListeInformations))]
    public virtual CodeReduction? InformationCodeReduction { get; set; }

    [ForeignKey(nameof(RetraitMagasinId))]
    [InverseProperty(nameof(RetraitMagasin.ListeInformations))]
    public virtual RetraitMagasin? InformationRetraitMagasin { get; set; }

    [InverseProperty(nameof(RetraitMagasin.RetraitMagasinInformation))]
    public virtual ICollection<RetraitMagasin> ListeRetraitMagasins { get; set; } = new List<RetraitMagasin>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idvelo", "Idinspection", "Idreparation")]
[Table("t_j_estrealise_esr", Schema = "upways")]
[Index("Idinspection", Name = "idx_estrealise_idinspection")]
[Index("Idreparation", Name = "idx_estrealise_idreparation")]
[Index("Idvelo", Name = "idx_estrealise_idvelo")]
public partial class Estrealise
{
    [Key]
    [Column("esr_id")]
    public int VeloId { get; set; }

    [Key]
    [Column("esr_idInspection")]
    public int InspectionId { get; set; }

    [Key]
    [Column("esr_idReparation")]
    public int ReparationId { get; set; }

    [Column("esr_dateinspection", TypeName = "date")]
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "la date doit être au format français")]
    public DateTime DateInspection { get; set; }

    [Column("esr_commentaireInspection", TypeName = "text")]
    [StringLength(4096)]
    public string? CommentaireInspection { get; set; }

    [Column("esr_historiqueInspection")]
    [StringLength(100)]
    public string? HistoriqueInspection { get; set; }

    [ForeignKey(nameof(InspectionId))]
    [InverseProperty(nameof(Rapportinspection.ListeEstRealises))]
    public virtual Rapportinspection EstRealiseRapportInspection { get; set; } = null!;

    [ForeignKey(nameof(ReparationId))]
    [InverseProperty(nameof(Reparationvelo.ListeEstRealises))]
    public virtual Reparationvelo EstRealiseReparationVelo { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeEstRealises))]
    public virtual Velo EstRealiseVelo { get; set; } = null!;
}

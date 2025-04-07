using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey(nameof(VeloId), nameof(InspectionId), nameof(ReparationId))]
[Table("t_j_estrealise_esr", Schema = "upways")]
[Index(nameof(InspectionId), Name = "ix_t_j_estrealise_esr_inspectionid")]
[Index(nameof(ReparationId), Name = "ix_t_j_estrealise_esr_reparationid")]
[Index(nameof(VeloId), Name = "ix_t_j_estrealise_esr_veloid")]
public class EstRealise : ISizedEntity
{
    [Key] [Column("vel_id")] public int VeloId { get; set; }

    [Key] [Column("ras_id")] public int InspectionId { get; set; }

    [Key] [Column("esr_id")] public int ReparationId { get; set; }

    [Column("esr_dateinspection")] public string? DateInspection { get; set; }

    [Column("esr_commentaireinspection", TypeName = "text")]
    [StringLength(4096)]
    public string? CommentaireInspection { get; set; }

    [Column("esr_historiqueinspection")]
    [StringLength(100)]
    public string? HistoriqueInspection { get; set; }

    [ForeignKey(nameof(InspectionId))]
    [InverseProperty(nameof(RapportInspection.ListeEstRealises))]
    public virtual RapportInspection EstRealiseRapportInspection { get; set; } = null!;

    [ForeignKey(nameof(ReparationId))]
    [InverseProperty(nameof(ReparationVelo.ListeEstRealises))]
    public virtual ReparationVelo EstRealiseReparationVelo { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeEstRealises))]
    public virtual Velo EstRealiseVelo { get; set; } = null!;

    public long GetSize()
    {
        return sizeof(int) * 3 + DateInspection?.Length ??
               0 + CommentaireInspection?.Length ?? 0 + HistoriqueInspection?.Length ?? 0;
    }
}
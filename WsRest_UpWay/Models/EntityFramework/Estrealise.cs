using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_j_estrealise_ere")]
public class Estrealise
{
    [Key] [Column("ere_velo_id")] public int BicycleId { get; set; }

    [Key] [Column("ere_inspection_id")] public int InspectionId { get; set; }

    [Key] [Column("ere_reparation_id")] public int RepairId { get; set; }

    [Column] public DateTime? InspectionDate { get; set; }

    [Column("ere_commentaire_inspection")]
    [StringLength(4096)]
    public string? InspectionComment { get; set; }

    [Column("ere_historique_inspection")]
    [StringLength(100)]
    public string? InspectionHistory { get; set; }

    public virtual Rapportinspection InspectionReport { get; set; } = null!;

    public virtual Reparationvelo BicyleRepair { get; set; } = null!;

    public virtual Velo Bicycle { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_rapportinspection_ras", Schema = "upways")]
public class RapportInspection : ISizedEntity
{
    [Key] [Column("ras_id")] public int InspectionId { get; set; }

    [Column("ras_type")]
    [StringLength(200)]
    public string? TypeInspection { get; set; }

    [Column("ras_soustype")]
    [StringLength(200)]
    public string? SousTypeInspection { get; set; }

    [Column("ras_pointdinspection")]
    [StringLength(200)]
    public string? PointDInspection { get; set; }

    [InverseProperty(nameof(EstRealise.EstRealiseRapportInspection))]
    public virtual ICollection<EstRealise> ListeEstRealises { get; set; } = new List<EstRealise>();

    public long GetSize()
    {
        return sizeof(int) + TypeInspection?.Length ??
               0 + SousTypeInspection?.Length ?? 0 + PointDInspection?.Length ?? 0;
    }
}
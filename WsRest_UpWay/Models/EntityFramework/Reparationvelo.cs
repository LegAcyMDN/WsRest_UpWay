using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_reparationvelo_rev", Schema = "upways")]
public class ReparationVelo : ISizedEntity
{
    [Key] [Column("rev_id")] public int ReparationId { get; set; }

    [Column("rev_check")] public bool? CheckReparation { get; set; }

    [Column("rev_checkvalidation")] public bool? CheckValidation { get; set; }

    [InverseProperty(nameof(EstRealise.EstRealiseReparationVelo))]
    public virtual ICollection<EstRealise> ListeEstRealises { get; set; } = new List<EstRealise>();

    public long GetSize()
    {
        return sizeof(int) + sizeof(bool) * 2;
    }
}
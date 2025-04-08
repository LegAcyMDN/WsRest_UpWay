using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey(nameof(UtiliteId), nameof(VeloId))]
[Table("t_j_utilite_uti", Schema = "upways")]
[Index(nameof(VeloId), Name = "ix_t_e_utilite_uti_veloid")]
public class Utilite : ISizedEntity
{
    [Key] [Column("uti_id")] public int UtiliteId { get; set; }

    [Key] [Column("vel_id")] public int VeloId { get; set; }

    [Column("uti_valeur")]
    [Precision(5, 0)]
    public decimal? ValeurUtilite { get; set; }

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeUtilites))]
    public virtual Velo UtiliteVelo { get; set; } = null!;

    public long GetSize()
    {
        return sizeof(int) * 2 + sizeof(decimal);
    }
}
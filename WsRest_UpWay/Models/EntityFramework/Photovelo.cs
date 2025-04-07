using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_photovelo_phv", Schema = "upways")]
[Index(nameof(VeloId), Name = "ix_t_e_photovelo_phv_veloid")]
public class PhotoVelo : ISizedEntity
{
    public const long APROXIMATE_SIZE = 8;

    [Key] [Column("phv_id")] public int PhotoVeloId { get; set; }

    [Key] [Column("vel_id")] public int VeloId { get; set; }

    [Column("phv_url", TypeName = "text")]
    [StringLength(4096)]
    public string? UrlPhotoVelo { get; set; }

    [Column("phv_bytea")] public byte[]? PhotoBytea { get; set; }

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListePhotoVelos))]
    public virtual Velo PhotoVeloVelo { get; set; } = null!;

    public long GetSize()
    {
        return sizeof(int) * 2 + UrlPhotoVelo?.Length ?? 0 + PhotoBytea?.Length ?? 0;
    }
}
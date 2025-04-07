using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_photoaccessoire_pha", Schema = "upways")]
[Index(nameof(AccessoireId), Name = "ix_t_e_photoaccessoire_pha_accessoireid")]
public class PhotoAccessoire : ISizedEntity
{
    [Key] [Column("pha_id")] public int PhotoAcessoireId { get; set; }

    [Key] [Column("acs_id")] public int AccessoireId { get; set; }

    [Column("pha_url", TypeName = "text")]
    [StringLength(4096)]
    public string? UrlPhotoAccessoire { get; set; }

    [ForeignKey(nameof(AccessoireId))]
    [InverseProperty(nameof(Accessoire.ListePhotoAccessoires))]
    public virtual Accessoire PhotoAccessoireAccessoire { get; set; } = null!;

    public long GetSize()
    {
        return sizeof(int) * 2 + UrlPhotoAccessoire?.Length ?? 0;
    }
}
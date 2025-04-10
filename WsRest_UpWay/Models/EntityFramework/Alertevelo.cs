using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey(nameof(AlerteId), nameof(ClientId), nameof(VeloId))]
[Table("t_e_alertevelo_alv", Schema = "upways")]
[Index(nameof(ClientId), Name = "ix_t_e_alertevelo_alv_clientid")]
[Index(nameof(VeloId), Name = "ix_t_e_alertevelo_alv_veloid")]
public class AlerteVelo : ISizedEntity
{
    [Key] [Column("alv_id")] public int AlerteId { get; set; }

    [Key] [Column("cli_id")] public int ClientId { get; set; }

    [Key] [Column("vel_id")] public int VeloId { get; set; }

    [Column("alv_modification", TypeName = "date")]
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$",
        ErrorMessage = "la date doit être au format français")]
    public DateTime? ModificationAlerte { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(CompteClient.ListeAlerteVelos))]
    public virtual CompteClient? AlerteClient { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeAlerteVelos))]
    public virtual Velo? AlerteVeloVelo { get; set; } = null!;

    public long GetSize()
    {
        return sizeof(int) * 3 + sizeof(long);
    }
}
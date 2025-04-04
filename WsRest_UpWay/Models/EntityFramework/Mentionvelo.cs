using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_mentionvelo_mev", Schema = "upways")]
[Index(nameof(VeloId), Name = "ix_t_e_mentionvelo_mev_veloid")]
public class MentionVelo
{
    public const long APROXIMATE_SIZE = 4;

    [Key] [Column("mev_id")] public int MentionId { get; set; }

    [Column("vel_id")] public int VeloId { get; set; }

    [Column("mev_libelle")]
    [StringLength(50)]
    public string? LibelleMention { get; set; }

    [Column("mev_valeur", TypeName = "text")]
    [StringLength(4096)]
    public string? ValeurMention { get; set; }

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeMentionVelos))]
    public virtual Velo MentionVeloVelo { get; set; } = null!;
}
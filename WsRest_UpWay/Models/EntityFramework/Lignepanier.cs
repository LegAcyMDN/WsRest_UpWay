using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey(nameof(PanierId), nameof(VeloId))]
[Table("t_e_lignepanier_lignpan", Schema = "upways")]
[Index(nameof(AssuranceId), Name = "ix_t_e_linepanier_lignpan_assuranceid")]
[Index(nameof(PanierId), Name = "ix_t_e_linepanier_lignpan_panierid")]
[Index(nameof(VeloId), Name = "ix_t_e_linepanier_lignpan_veloid")]
public class LignePanier
{
    public LignePanier()
    {
        ListeMarquageVelos = new HashSet<MarquageVelo>();
    }

    [Key] [Column("lignpan_id")] public int PanierId { get; set; }

    [Key] [Column("vel_id")] public int VeloId { get; set; }

    [Column("ass_id")] public int AssuranceId { get; set; }

    [Column("lignpan_quantpan")]
    [Precision(2, 0)]
    public decimal QuantitePanier { get; set; }

    [Column("lignpan_priquant")]
    [Precision(11, 2)]
    public decimal PrixQuantite { get; set; }

    [ForeignKey(nameof(AssuranceId))]
    [InverseProperty(nameof(Assurance.ListeLignePaniers))]
    public virtual Assurance LignePanierAssurance { get; set; } = null!;

    [ForeignKey(nameof(PanierId))]
    [InverseProperty(nameof(Panier.ListeLignePaniers))]
    public virtual Panier LignePanierPanier { get; set; } = null!;

    [ForeignKey(nameof(VeloId))]
    [InverseProperty(nameof(Velo.ListeLignePaniers))]
    public virtual Velo LignePanierVelo { get; set; } = null!;

    [InverseProperty(nameof(MarquageVelo.MarquageVeloLignePanier))]
    public virtual ICollection<MarquageVelo> ListeMarquageVelos { get; set; } = new List<MarquageVelo>();
}
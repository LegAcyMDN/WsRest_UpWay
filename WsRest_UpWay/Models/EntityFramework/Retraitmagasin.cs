using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_retraitmagasin_rem", Schema = "upways")]
[Index(nameof(CommandeId), Name = "ix_t_e_retraitmagasin_rem_commandeid")]
[Index(nameof(InformationId), Name = "ix_t_e_retraitmagasin_rem_informationid")]
[Index(nameof(MagasinId), Name = "ix_t_e_retraitmagasin_rem_magasinid")]
public class RetraitMagasin
{
    public RetraitMagasin()
    {
        ListeDetailCommandes = new HashSet<DetailCommande>();
        ListeInformations = new HashSet<Information>();
    }

    [Key] [Column("rem_id")] public int RetraitMagasinId { get; set; }

    [Column("inf_id")] public int? InformationId { get; set; }

    [Column("detcom_id")] public int? CommandeId { get; set; }

    [Column("mag_id")] public int MagasinId { get; set; }

    [Column("rem_date", TypeName = "date")]
    public DateOnly? DateRetrait { get; set; }

    [Column("rem_heure", TypeName = "time")]
    public TimeOnly? HeureRetrait { get; set; }

    [ForeignKey(nameof(CommandeId))]
    [InverseProperty(nameof(DetailCommande.ListeRetraitMagasins))]
    public virtual DetailCommande? RetraitMagasinDetailCom { get; set; }

    [ForeignKey(nameof(InformationId))]
    [InverseProperty(nameof(Information.ListeRetraitMagasins))]
    public virtual Information? RetraitMagasinInformation { get; set; }

    [ForeignKey(nameof(MagasinId))]
    [InverseProperty(nameof(Magasin.ListeRetraitMagasins))]
    public virtual Magasin RetraitMagasinMagasin { get; set; } = null!;

    [InverseProperty(nameof(DetailCommande.DetailComRetraitMagasin))]
    public virtual ICollection<DetailCommande> ListeDetailCommandes { get; set; } = new List<DetailCommande>();

    [InverseProperty(nameof(Information.InformationRetraitMagasin))]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();
}
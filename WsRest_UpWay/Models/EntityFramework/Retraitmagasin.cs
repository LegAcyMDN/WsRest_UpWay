using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_retraitmagasin_rem", Schema = "upways")]
[Index("Idcommande", Name = "idx_retraitmagasin_idcommande")]
[Index("Idinformations", Name = "idx_retraitmagasin_idinformations")]
[Index("Idmagasin", Name = "idx_retraitmagasin_idmagasin")]
public class Retraitmagasin
{
    public Retraitmagasin()
    {
        ListeDetailCommandes = new HashSet<Detailcommande>();
    }

    [Key] [Column("rem_id")] public int RetraitMagasinId { get; set; }

    [Column("rem_idinformations")] public int? InformationId { get; set; }

    [Column("rem_idcommande")] public int? CommandeId { get; set; }

    [Column("rem_idmagasin")] public int MagasinId { get; set; }

    [Column("rem_date", TypeName = "date")]
    public DateOnly? DateRetrait { get; set; }

    [Column("rem_heure", TypeName = "heure")]
    public TimeOnly? HeureRetrait { get; set; }

    [ForeignKey(nameof(CommandeId))]
    [InverseProperty(nameof(Detailcommande.RetraitMagasinId))]
    public virtual Detailcommande? RetraitMagasinDetailCom { get; set; }

    [ForeignKey(nameof(InformationId))]
    [InverseProperty(nameof(Information.InformationRetraitMagasin))]
    public virtual Information? RetraitMagasinInformation { get; set; }

    [ForeignKey(nameof(MagasinId))]
    [InverseProperty(nameof(Magasin.ListeRetraitMagasins))]
    public virtual Magasin RetraitMagasinMagasin { get; set; } = null!;

    [InverseProperty(nameof(Detailcommande.DetailComRetraitMagasin))]
    public virtual ICollection<Detailcommande> ListeDetailCommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty(nameof(Information.InformationRetraitMagasin))]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();
}
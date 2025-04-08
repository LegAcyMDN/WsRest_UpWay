using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_adresseexpedition_ade", Schema = "upways")]
[Index(nameof(AdresseFactId), Name = "ix_t_e_adresseexpedition_ade_adressefactid")]
[Index(nameof(ClientId), Name = "ix_t_e_adresseexpedition_ade_clientid")]
public class AdresseExpedition : ISizedEntity
{
    public AdresseExpedition()
    {
        ListeAdresseFact = new HashSet<AdresseFacturation>();
        ListeInformations = new HashSet<Information>();
    }

    [Key] [Column("ade_id")] public int AdresseExpeId { get; set; }

    [Column("cli_id")] public int ClientId { get; set; }

    [Column("adf_id")] public int? AdresseFactId { get; set; }

    [Column("ade_pays")]
    [StringLength(50)]
    public string? PaysExpedition { get; set; }

    [Column("ade_batopt")]
    [StringLength(100)]
    public string? BatimentExpeditionOpt { get; set; }

    [Column("ade_rue")]
    [StringLength(200)]
    public string? RueExpedition { get; set; }

    [Column("ade_cp", TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Le code postal doit être composé de 5 chiffres.")]
    public string? CPExpedition { get; set; }

    [Column("ade_region")]
    [StringLength(20)]
    public string? RegionExpedition { get; set; }

    [Column("ade_ville")]
    [StringLength(100)]
    public string? VilleExpedition { get; set; }

    [Column("ade_telephone", TypeName = "char(14)")]
    [StringLength(14, ErrorMessage = "Le code postal doit être composé de 10 chiffres.")]
    public string? TelephoneExpedition { get; set; }

    [Column("ade_donneessauv")] public bool? DonneesSauvegardees { get; set; }

    [ForeignKey(nameof(AdresseFactId))]
    [InverseProperty(nameof(AdresseFacturation.ListeAdresseExpe))]
    public virtual AdresseFacturation? AdresseExpeFact { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(CompteClient.ListeAdresseExpe))]
    public virtual CompteClient AdresseExpeClient { get; set; } = null!;

    [InverseProperty(nameof(AdresseFacturation.AdresseFactExpe))]
    public virtual ICollection<AdresseFacturation> ListeAdresseFact { get; set; } = new List<AdresseFacturation>();

    [InverseProperty(nameof(Information.InformationAdresseExpe))]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();

    public long GetSize()
    {
        return sizeof(int) * 3 +
            PaysExpedition?.Length ?? 0 +
            BatimentExpeditionOpt?.Length ?? 0 +
            RueExpedition?.Length ?? 0 +
            CPExpedition?.Length +
            RegionExpedition?.Length ?? 0 +
            VilleExpedition?.Length ?? 0 +
            TelephoneExpedition?.Length ?? 0 +
            sizeof(bool);
    }
}
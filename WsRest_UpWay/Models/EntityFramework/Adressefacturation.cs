using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_adressefacturation_adf", Schema = "upways")]
[Index(nameof(AdresseExpId), Name = "ix_t_e_adressefacturation_adf_adresseexpeid")]
[Index(nameof(ClientId), Name = "ix_t_e_adressefacturation_adf_clientid")]
public class AdresseFacturation : ISizedEntity
{
    public AdresseFacturation()
    {
        ListeAdresseExpe = new HashSet<AdresseExpedition>();
        ListeDetailCommande = new HashSet<DetailCommande>();
    }

    [Key] [Column("adf_id")] public int AdresseFactId { get; set; }

    [Column("cli_id")] public int ClientId { get; set; }

    [Column("ade_id")] public int? AdresseExpId { get; set; }

    [Column("adf_pays")]
    [StringLength(50)]
    public string? PaysFacturation { get; set; }

    [Column("adf_batopt")]
    [StringLength(100)]
    public string? BatimentFacturationOpt { get; set; }

    [Column("adf_rue")]
    [StringLength(200)]
    public string? RueFacturation { get; set; }

    [Column("adf_cp", TypeName = "char(5)")]
    [StringLength(5)]
    public string? CPFacturation { get; set; }

    [Column("adf_region")]
    [StringLength(20)]
    public string? RegionFacturation { get; set; }

    [Column("adf_ville")]
    [StringLength(100)]
    public string? VilleFacturation { get; set; }

    [Column("adf_telephone", TypeName = "char(14)")]
    [StringLength(14)]
    public string? TelephoneFacturation { get; set; }

    [ForeignKey(nameof(AdresseExpId))]
    [InverseProperty(nameof(AdresseExpedition.ListeAdresseFact))]
    public virtual AdresseExpedition? AdresseFactExpe { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(CompteClient.ListeAdresseFact))]
    public virtual CompteClient AdresseFactClient { get; set; } = null!;

    [InverseProperty(nameof(AdresseExpedition.AdresseExpeFact))]
    public virtual ICollection<AdresseExpedition> ListeAdresseExpe { get; set; } = new List<AdresseExpedition>();

    [InverseProperty(nameof(DetailCommande.DetailComAdresseFact))]
    public virtual ICollection<DetailCommande> ListeDetailCommande { get; set; } = new List<DetailCommande>();

    public long GetSize()
    {
        return sizeof(int) * 3 +
            PaysFacturation?.Length ?? 0 +
            BatimentFacturationOpt?.Length ?? 0 +
            RueFacturation?.Length ?? 0 +
            CPFacturation?.Length +
            RegionFacturation?.Length ?? 0 +
            VilleFacturation?.Length ?? 0 +
            TelephoneFacturation?.Length ?? 0;
    }
}
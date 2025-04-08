using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_compteclient_coc", Schema = "upways")]
[Index(nameof(EmailClient), Name = "ix_t_e_compteclient_coc_email_unq", IsUnique = true)]
[Index(nameof(LoginClient), Name = "ix_t_e_compteclient_coc_pseudo_unq", IsUnique = true)]
public partial class CompteClient : ISizedEntity
{
    public CompteClient()
    {
        ListeAdresseExpe = new HashSet<AdresseExpedition>();
        ListeAdresseFact = new HashSet<AdresseFacturation>();
        ListeAlerteVelos = new HashSet<AlerteVelo>();
        ListeDetailCommandes = new HashSet<DetailCommande>();
        ListePaniers = new HashSet<Panier>();
    }

    [Key] [Column("coc_id")] public int ClientId { get; set; }

    [Column("coc_login")]
    [StringLength(20)]
    [Required]
    public string? LoginClient { get; set; }

    [Column("coc_mpd")]
    [StringLength(128)]
    [Required]
    public string? MotDePasseClient { get; set; }

    [Column("coc_email")]
    [StringLength(200)]
    [EmailAddress]
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "L'adresse mail ne correspond pas ")]
    public string? EmailClient { get; set; }

    [Column("coc_prenom")]
    [StringLength(20)]
    public string? PrenomClient { get; set; }

    [Column("coc_nom")] [StringLength(30)] public string? NomClient { get; set; }

    [Column("coc_datecreation", TypeName = "date")]
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$",
        ErrorMessage = "la date doit étre en format français")]
    public DateOnly? DateCreation { get; set; }

    [Column("coc_remember_token")]
    [StringLength(100)]
    public string? RememberToken { get; set; }

    [Column("coc_two_factor_secret", TypeName = "text")]
    [StringLength(4096)]
    public string? TwoFactorSecret { get; set; }

    [Column("coc_two_factor_recovery_codes", TypeName = "text")]
    [StringLength(4096)]
    public string? TwoFactorRecoveryCodes { get; set; }

    [Column("coc_two_factor_confirmed_at", TypeName = "date")]
    public DateTime? TwoFactorConfirmedAt { get; set; }

    [Column("coc_usertype")]
    [StringLength(20)]
    public string? Usertype { get; set; }

    [Column("coc_email_verified_at", TypeName = "date")]
    public DateOnly? EmailVerifiedAt { get; set; }

    [Column("coc_is_from_google")] public bool? IsFromGoogle { get; set; }

    [InverseProperty(nameof(AdresseExpedition.AdresseExpeClient))]
    public virtual ICollection<AdresseExpedition> ListeAdresseExpe { get; set; } = new List<AdresseExpedition>();

    [InverseProperty(nameof(AdresseFacturation.AdresseFactClient))]
    public virtual ICollection<AdresseFacturation> ListeAdresseFact { get; set; } = new List<AdresseFacturation>();

    [InverseProperty(nameof(AlerteVelo.AlerteClient))]
    public virtual ICollection<AlerteVelo> ListeAlerteVelos { get; set; } = new List<AlerteVelo>();

    [InverseProperty(nameof(DetailCommande.DetailCommandeClient))]
    public virtual ICollection<DetailCommande> ListeDetailCommandes { get; set; } = new List<DetailCommande>();

    [InverseProperty(nameof(Panier.PanierClient))]
    public virtual ICollection<Panier> ListePaniers { get; set; } = new List<Panier>();

    public long GetSize()
    {
        return sizeof(int) +
            LoginClient?.Length ?? 0 +
            MotDePasseClient?.Length ?? 0 +
            EmailClient?.Length ?? 0 +
            PrenomClient?.Length ?? 0 +
            NomClient?.Length ?? 0 +
            sizeof(long) +
            RememberToken?.Length ?? 0 +
            TwoFactorSecret?.Length ?? 0 +
            TwoFactorRecoveryCodes?.Length ?? 0 +
            sizeof(long) +
            Usertype?.Length ?? 0 +
            sizeof(long);
    }
}
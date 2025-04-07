using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_panier_pan", Schema = "upways")]
[Index(nameof(CommandeId), Name = "ix_t_e_panier_pan_commandeid")]
public class Panier : ISizedEntity
{
    public Panier()
    {
        ListeAjouterAccessoires = new HashSet<AjouterAccessoire>();
        ListeDetailCommandes = new HashSet<DetailCommande>();
        ListeInformations = new HashSet<Information>();
        ListeLignePaniers = new HashSet<LignePanier>();
    }

    [Key] [Column("pan_id")] public int PanierId { get; set; }


    [Column("cli_id")] public int? ClientId { get; set; }


    [Column("com_id")] public int? CommandeId { get; set; }

    [Column("pan_cookie", TypeName = "text")]
    [StringLength(255)]
    public string? Cookie { get; set; }

    [Column("pan_prix")]
    [Precision(11, 2)]
    public decimal? PrixPanier { get; set; }

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(CompteClient.ListePaniers))]
    public virtual CompteClient? PanierClient { get; set; }

    [ForeignKey(nameof(CommandeId))]
    [InverseProperty(nameof(DetailCommande.ListePaniers))]
    public virtual DetailCommande? PanierDetailCommande { get; set; }

    [InverseProperty(nameof(AjouterAccessoire.AjoutDAccessoirePanier))]
    public virtual ICollection<AjouterAccessoire> ListeAjouterAccessoires { get; set; } = new List<AjouterAccessoire>();

    [InverseProperty(nameof(DetailCommande.DetailCommandePanier))]
    public virtual ICollection<DetailCommande> ListeDetailCommandes { get; set; } = new List<DetailCommande>();

    [InverseProperty(nameof(Information.InformationPanier))]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();

    [InverseProperty(nameof(LignePanier.LignePanierPanier))]
    public virtual ICollection<LignePanier> ListeLignePaniers { get; set; } = new List<LignePanier>();

    public long GetSize()
    {
        return sizeof(int) * 3 + Cookie?.Length ?? 0 + sizeof(decimal);
    }
}
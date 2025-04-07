using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_etatcommande_etc", Schema = "upways")]
public class EtatCommande : ISizedEntity
{
    public EtatCommande()
    {
        ListeDetailCommandes = new HashSet<DetailCommande>();
    }

    [Key] [Column("etc_id")] public int EtatCommandeId { get; set; }

    [Column("etc_libelle")]
    [StringLength(20)]
    public string? LibelleEtat { get; set; }

    [InverseProperty(nameof(DetailCommande.DetailCommandeEtat))]
    public virtual ICollection<DetailCommande> ListeDetailCommandes { get; set; } = new List<DetailCommande>();

    public long GetSize()
    {
        return sizeof(int) + LibelleEtat?.Length ?? 0;
    }
}
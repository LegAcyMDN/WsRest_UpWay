using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_accessoire_acs", Schema = "upways")]
[Index(nameof(CategorieId), Name = "ix_t_e_accessoire_acs_categorieid")]
[Index(nameof(MarqueId), Name = "ix_t_e_accessoire_acs_marqueid")]
public class Accessoire : ISizedEntity
{
    public Accessoire()
    {
        ListeAjoutAccessoires = new HashSet<AjouterAccessoire>();
        ListePhotoAccessoires = new HashSet<PhotoAccessoire>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key] [Column("acs_id")] public int AccessoireId { get; set; }

    [Key] [Column("mar_id")] public int MarqueId { get; set; }

    [Key] [Column("cat_id")] public int CategorieId { get; set; }

    [Column("acs_nom")]
    [StringLength(100)]
    public string? NomAccessoire { get; set; }

    [Column("acs_prix", TypeName = "numeric(8, 2)")]
    [Precision(8, 2)]
    public decimal? PrixAccessoire { get; set; }

    [Column("acs_description", TypeName = "text")]
    [StringLength(4096)]
    public string? DescriptionAccessoire { get; set; }

    [ForeignKey(nameof(CategorieId))]
    [InverseProperty(nameof(Categorie.ListeAccessoires))]
    public virtual Categorie? AccessoireCategorie { get; set; } = null;

    [ForeignKey(nameof(MarqueId))]
    [InverseProperty(nameof(Marque.ListeAccessoires))]
    public virtual Marque? AccessoireMarque { get; set; } = null!;

    [InverseProperty(nameof(AjouterAccessoire.AjoutDAccessoire))]
    public virtual ICollection<AjouterAccessoire> ListeAjoutAccessoires { get; set; } = new List<AjouterAccessoire>();

    [InverseProperty(nameof(PhotoAccessoire.PhotoAccessoireAccessoire))]
    public virtual ICollection<PhotoAccessoire> ListePhotoAccessoires { get; set; } = new List<PhotoAccessoire>();

    [ForeignKey(nameof(AccessoireId))]
    [InverseProperty(nameof(Velo.ListeAccessoires))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();

    public long GetSize()
    {
        return sizeof(int) * 3 + sizeof(decimal) + NomAccessoire?.Length ?? 0 + DescriptionAccessoire?.Length ?? 0;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_caracteristique_car", Schema = "upways")]
public class Caracteristique : ISizedEntity
{
    public Caracteristique()
    {
        ListeSousCaracteristiques = new HashSet<Caracteristique>();
        ListeCaracteristiques = new HashSet<Caracteristique>();
        ListeCategories = new HashSet<Categorie>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key] [Column("car_id")] public int CaracteristiqueId { get; set; }

    [Column("car_libelle")]
    [StringLength(100)]
    public string? LibelleCaracteristique { get; set; }

    [Column("car_image")]
    [StringLength(200)]
    public string? ImageCaracteristique { get; set; }

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(ListeCaracteristiques))]
    public virtual ICollection<Caracteristique> ListeSousCaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(ListeSousCaracteristiques))]
    public virtual ICollection<Caracteristique> ListeCaracteristiques { get; set; } = new List<Caracteristique>();

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(Categorie.ListeCaracteristiques))]
    public virtual ICollection<Categorie> ListeCategories { get; set; } = new List<Categorie>();

    [ForeignKey(nameof(CaracteristiqueId))]
    [InverseProperty(nameof(Velo.ListeCaracteristiques))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();

    public long GetSize()
    {
        return sizeof(int) + LibelleCaracteristique?.Length ?? 0 + ImageCaracteristique?.Length ?? 0;
    }
}
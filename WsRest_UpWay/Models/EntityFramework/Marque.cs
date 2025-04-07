using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_marque_mar", Schema = "upways")]
[Index(nameof(NomMarque), Name = "nommarque_unq", IsUnique = true)]
public class Marque : ISizedEntity
{
    public Marque()
    {
        ListeAccessoires = new HashSet<Accessoire>();
        ListeMoteurs = new HashSet<Moteur>();
        ListeVeloModifiers = new HashSet<VeloModifier>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key] [Column("mar_id")] public int MarqueId { get; set; }

    [Column("mar_nom")]
    [StringLength(100)]
    public string? NomMarque { get; set; }

    [InverseProperty(nameof(Accessoire.AccessoireMarque))]
    public virtual ICollection<Accessoire> ListeAccessoires { get; set; } = new List<Accessoire>();

    [InverseProperty(nameof(Moteur.MoteurMarque))]
    public virtual ICollection<Moteur> ListeMoteurs { get; set; } = new List<Moteur>();

    [InverseProperty(nameof(VeloModifier.VeloModifierMarque))]
    public virtual ICollection<VeloModifier> ListeVeloModifiers { get; set; } = new List<VeloModifier>();

    [InverseProperty(nameof(Velo.VeloMarque))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();

    public long GetSize()
    {
        return sizeof(int) + NomMarque?.Length ?? 0;
    }
}
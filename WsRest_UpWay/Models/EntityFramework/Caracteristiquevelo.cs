using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_caracteristiquevelo_cav", Schema = "upways")]
public class CaracteristiqueVelo : ISizedEntity
{
    public CaracteristiqueVelo()
    {
        ListeVeloModifiers = new HashSet<VeloModifier>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key] [Column("cav_id")] public int CaracteristiqueVeloId { get; set; }

    [Column("cav_poids")]
    [Precision(5, 2)]
    [RegularExpression(@"^\d{1,4}(\.\d{2})?$",
        ErrorMessage =
            "le poids n'est pas valide il doit avoir entre 1 et 4 chiffres avant la virgule qui est un point et 2 chiffres obligatoires après.")]
    public decimal Poids { get; set; }

    [Column("cav_tubeselle")] public int TubeSelle { get; set; }

    [Column("cav_typesuspension")]
    [StringLength(20)]
    public string? TypeSuspension { get; set; }

    [Column("cav_couleur")]
    [StringLength(10)]
    public string? Couleur { get; set; }

    [Column("cav_typecargo")]
    [StringLength(20)]
    public string? TypeCargo { get; set; }

    [Column("cav_etatbatterie")]
    [StringLength(10)]
    public string? EtatBatterie { get; set; }

    [Column("cav_nombrecycle")] public int? NombreCycle { get; set; }

    [Column("cav_materiau")]
    [StringLength(20)]
    public string? Materiau { get; set; }

    [Column("cav_fourche")]
    [StringLength(50)]
    public string? Fourche { get; set; }

    [Column("cav_debattement")] public int? Debattement { get; set; }

    [Column("cav_amortisseur")]
    [StringLength(50)]
    public string? Amortisseur { get; set; }

    [Column("cav_debattementamortisseur")] public int? DebattementAmortisseur { get; set; }

    [Column("cav_modeltransmission")]
    [StringLength(50)]
    public string? ModelTransmission { get; set; }

    [Column("cav_nombrevitesse")] public int? NombreVitesse { get; set; }

    [Column("cav_freins")]
    [StringLength(30)]
    public string? Freins { get; set; }

    [Column("cav_taillesroues")] public int? TaillesRoues { get; set; }

    [Column("cav_pneus")]
    [StringLength(100)]
    public string? Pneus { get; set; }

    [Column("cav_selletelescopique")] public bool? SelleTelescopique { get; set; }

    [InverseProperty(nameof(VeloModifier.VeloModifCaracteristiqueVelo))]
    public virtual ICollection<VeloModifier> ListeVeloModifiers { get; set; } = new List<VeloModifier>();

    [InverseProperty(nameof(Velo.VeloCaracteristiqueVelo))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();

    public long GetSize()
    {
        return sizeof(int) * 7;
    }
}
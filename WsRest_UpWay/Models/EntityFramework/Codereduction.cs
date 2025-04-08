using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_codereduction_cor", Schema = "upways")]
public class CodeReduction : ISizedEntity
{
    public CodeReduction()
    {
        ListeInformations = new HashSet<Information>();
    }

    [Key]
    [Column("cor_id")]
    [StringLength(20)]
    public string ReductionId { get; set; } = null!;

    [Column("cor_actifreduction")] public bool? ActifReduction { get; set; }

    [Column("cor_reduction")] public int? Reduction { get; set; }

    [InverseProperty(nameof(Information.InformationCodeReduction))]
    public virtual ICollection<Information> ListeInformations { get; set; } = new List<Information>();

    public long GetSize()
    {
        return sizeof(int) + sizeof(bool) + ReductionId.Length;
    }
}
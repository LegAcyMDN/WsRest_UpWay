using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_categorie_article_caa", Schema = "upways")]
public class CategorieArticle : ISizedEntity
{
    public CategorieArticle()
    {
        ListeArticles = new HashSet<Article>();
        ListeSousCategorieArticles = new HashSet<CategorieArticle>();
        ListeCategorieArticles = new HashSet<CategorieArticle>();
    }

    [Key] [Column("caa_id")] public int CategorieArticleId { get; set; }

    [Column("caa_titre")]
    [StringLength(100)]
    public string? TitreCategorieArticle { get; set; }

    [Column("caa_contenue", TypeName = "text")]
    [StringLength(4096)]
    public string? ContenuCategorieArticle { get; set; }

    [Column("caa_image", TypeName = "text")]
    [StringLength(4096)]
    public string? ImageCategorie { get; set; }

    [InverseProperty(nameof(Article.ArticleCategorieArt))]
    public virtual ICollection<Article> ListeArticles { get; set; } = new List<Article>();

    [ForeignKey(nameof(CategorieArticleId))]
    [InverseProperty(nameof(ListeCategorieArticles))]
    public virtual ICollection<CategorieArticle> ListeSousCategorieArticles { get; set; } =
        new List<CategorieArticle>();

    [ForeignKey(nameof(CategorieArticleId))]
    [InverseProperty(nameof(ListeSousCategorieArticles))]
    public virtual ICollection<CategorieArticle> ListeCategorieArticles { get; set; } = new List<CategorieArticle>();

    public long GetSize()
    {
        return sizeof(int) + TitreCategorieArticle?.Length ??
               0 + ContenuCategorieArticle?.Length ?? 0 + ImageCategorie?.Length ?? 0;
    }
}
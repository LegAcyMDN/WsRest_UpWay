using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_article_art")]
public partial class Article
{
    [Key]
    [Column("art_id")]
    public int ArticleId { get; set; }

    [Key]
    [Column("caa_id")]
    public int? CategorieArticleId { get; set; }

    [ForeignKey(nameof(CategorieArticleId))]
    public virtual CategorieArticle? IdcategorieArticleNavigation { get; set; }

    public virtual ICollection<ContenuArticle> ContenuArticles { get; set; } = new List<ContenuArticle>();
}

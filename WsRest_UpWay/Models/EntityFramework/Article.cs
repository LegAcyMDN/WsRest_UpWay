﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_article_art", Schema = "upways")]
public class Article
{
    public Article()
    {
        ListeContenuArticles = new HashSet<ContenuArticle>();
    }

    [Key] [Column("art_id")] public int ArticleId { get; set; }

    [Key] [Column("caa_id")] public int CategorieArticleId { get; set; }

    [ForeignKey(nameof(CategorieArticleId))]
    [InverseProperty(nameof(CategorieArticle.ListeArticles))]
    public virtual CategorieArticle? ArticleCategorieArt { get; set; }

    [InverseProperty(nameof(ContenuArticle.ContenuArticleArt))]
    public virtual ICollection<ContenuArticle> ListeContenuArticles { get; set; } = new List<ContenuArticle>();
}
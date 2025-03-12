using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("categorie_article", Schema = "upways")]
public partial class CategorieArticle
{
    [Key]
    [Column("idcategorie_article")]
    public int IdcategorieArticle { get; set; }

    [Column("titrecategorie_article")]
    [StringLength(100)]
    public string? TitrecategorieArticle { get; set; }

    [Column("contenuecategorie_article")]
    [StringLength(4096)]
    public string? ContenuecategorieArticle { get; set; }

    [Column("imagecategorie")]
    [StringLength(4096)]
    public string? Imagecategorie { get; set; }

    [InverseProperty("IdcategorieArticleNavigation")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [ForeignKey("IdcategorieArticle")]
    [InverseProperty("IdcategorieArticles")]
    public virtual ICollection<CategorieArticle> CatIdcategorieArticles { get; set; } = new List<CategorieArticle>();

    [ForeignKey("CatIdcategorieArticle")]
    [InverseProperty("CatIdcategorieArticles")]
    public virtual ICollection<CategorieArticle> IdcategorieArticles { get; set; } = new List<CategorieArticle>();
}

using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class CategorieArticle
{
    public int IdcategorieArticle { get; set; }

    public string? TitrecategorieArticle { get; set; }

    public string? ContenuecategorieArticle { get; set; }

    public string? Imagecategorie { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<CategorieArticle> CatIdcategorieArticles { get; set; } = new List<CategorieArticle>();

    public virtual ICollection<CategorieArticle> IdcategorieArticles { get; set; } = new List<CategorieArticle>();
}

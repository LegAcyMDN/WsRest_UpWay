using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Article
{
    public int Idarticle { get; set; }

    public int? IdcategorieArticle { get; set; }

    public virtual ICollection<ContenuArticle> ContenuArticles { get; set; } = new List<ContenuArticle>();

    public virtual CategorieArticle? IdcategorieArticleNavigation { get; set; }
}

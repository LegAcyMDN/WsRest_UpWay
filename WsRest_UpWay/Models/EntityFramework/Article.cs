using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("article", Schema = "upways")]
public partial class Article
{
    [Key]
    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idcategorie_article")]
    public int? IdcategorieArticle { get; set; }

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<ContenuArticle> ContenuArticles { get; set; } = new List<ContenuArticle>();

    [ForeignKey("IdcategorieArticle")]
    [InverseProperty("Articles")]
    public virtual CategorieArticle? IdcategorieArticleNavigation { get; set; }
}

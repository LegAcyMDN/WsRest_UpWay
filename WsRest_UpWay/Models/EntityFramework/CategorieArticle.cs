﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_categorie_article_caa", Schema = "upways")]
public partial class CategorieArticle
{
    [Key]
    [Column("caa_id")]
    public int CategorieArticleId { get; set; }

    [Column("caa_titre")]
    [StringLength(100)]
    public string? TitreCategorieArticle { get; set; }

    [Column("caa_contenue", TypeName = "text")]
    [StringLength(4096)]
    public string? ContenuCategorieArticle { get; set; }

    [Column("caa_image", TypeName = "text")]
    [StringLength(4096)]
    public string? ImageCategorie { get; set; }

    [InverseProperty("IdcategorieArticleNavigation")]
    public virtual ICollection<Article> ListeArticles { get; set; } = new List<Article>();

    [ForeignKey("IdcategorieArticle")]
    [InverseProperty("IdcategorieArticles")]
    public virtual ICollection<CategorieArticle> ListeSousCategorieArticles { get; set; } = new List<CategorieArticle>();

    [ForeignKey("CatIdcategorieArticle")]
    [InverseProperty("CatIdcategorieArticles")]
    public virtual ICollection<CategorieArticle> ListeCategorieArticles { get; set; } = new List<CategorieArticle>();
}

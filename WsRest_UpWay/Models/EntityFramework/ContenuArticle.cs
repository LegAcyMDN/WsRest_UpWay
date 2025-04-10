﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_contenu_article_coa", Schema = "upways")]
public class ContenuArticle : ISizedEntity
{
    [Key] [Column("coa_id")] public int ContenueId { get; set; }

    [Key] [Column("art_id")] public int ArticleId { get; set; }

    [Column("coa_prioritecontenu")] public int? PrioriteContenu { get; set; }

    [Column("coa_typecontenu")]
    [StringLength(64)]
    public string? TypeContenu { get; set; }

    [Column("coa_contenu", TypeName = "text")]
    [StringLength(4096)]
    public string? Contenu { get; set; }

    [ForeignKey(nameof(ArticleId))]
    [InverseProperty(nameof(Article.ListeContenuArticles))]
    public virtual Article? ContenuArticleArt { get; set; } = null!;

    public long GetSize()
    {
        return sizeof(int) * 3 + TypeContenu?.Length ?? 0 + Contenu?.Length ?? 0;
    }
}
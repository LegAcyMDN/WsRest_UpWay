﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_contenu_article_coa", Schema = "upways")]
public partial class ContenuArticle
{
    [Key]
    [Column("coa_id")]
    public int ContenueId { get; set; }

    [Column("idarticle")]
    public int ArticleId { get; set; }

    [Column("coa_prioritecontenu")]
    public int? PrioriteContenu { get; set; }

    [Column("coa_typecontenu")]
    [StringLength(64)]
    public string? TypeContenu { get; set; }

    [Column("coa_contenu", TypeName = "text")]
    [StringLength(4096)]
    public string? Contenu { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("ContenuArticles")]
    public virtual Article IdarticleNavigation { get; set; } = null!;
}

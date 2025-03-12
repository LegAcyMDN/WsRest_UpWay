using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("contenu_article", Schema = "upways")]
public partial class ContenuArticle
{
    [Key]
    [Column("idcontenue")]
    public int Idcontenue { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("prioritecontenu")]
    public int? Prioritecontenu { get; set; }

    [Column("typecontenu")]
    [StringLength(64)]
    public string? Typecontenu { get; set; }

    [Column("contenu")]
    [StringLength(4096)]
    public string? Contenu { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("ContenuArticles")]
    public virtual Article IdarticleNavigation { get; set; } = null!;
}

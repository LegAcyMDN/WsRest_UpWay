using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class ContenuArticle
{
    public int Idcontenue { get; set; }

    public int Idarticle { get; set; }

    public int? Prioritecontenu { get; set; }

    public string? Typecontenu { get; set; }

    public string? Contenu { get; set; }

    public virtual Article IdarticleNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("reparationvelo", Schema = "upways")]
public partial class Reparationvelo
{
    [Key]
    [Column("idreparation")]
    public int Idreparation { get; set; }

    [Column("checkreparation")]
    public bool? Checkreparation { get; set; }

    [Column("checkvalidation")]
    public bool? Checkvalidation { get; set; }

    [InverseProperty("IdreparationNavigation")]
    public virtual ICollection<Estrealise> Estrealises { get; set; } = new List<Estrealise>();
}

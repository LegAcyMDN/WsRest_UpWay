using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("retraitmagasin", Schema = "upways")]
[Index("Idcommande", Name = "idx_retraitmagasin_idcommande")]
[Index("Idinformations", Name = "idx_retraitmagasin_idinformations")]
[Index("Idmagasin", Name = "idx_retraitmagasin_idmagasin")]
public partial class Retraitmagasin
{
    [Key]
    [Column("idretraitmagasin")]
    public int Idretraitmagasin { get; set; }

    [Column("idinformations")]
    public int? Idinformations { get; set; }

    [Column("idcommande")]
    public int? Idcommande { get; set; }

    [Column("idmagasin")]
    public int Idmagasin { get; set; }

    [Column("dateretrait")]
    public DateOnly? Dateretrait { get; set; }

    [Column("heureretrait")]
    public TimeOnly? Heureretrait { get; set; }

    [InverseProperty("IdretraitmagasinNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [ForeignKey("Idcommande")]
    [InverseProperty("Retraitmagasins")]
    public virtual Detailcommande? IdcommandeNavigation { get; set; }

    [ForeignKey("Idinformations")]
    [InverseProperty("Retraitmagasins")]
    public virtual Information? IdinformationsNavigation { get; set; }

    [ForeignKey("Idmagasin")]
    [InverseProperty("Retraitmagasins")]
    public virtual Magasin IdmagasinNavigation { get; set; } = null!;

    [InverseProperty("IdretraitmagasinNavigation")]
    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}

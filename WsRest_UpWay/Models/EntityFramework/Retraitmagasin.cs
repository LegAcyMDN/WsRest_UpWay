using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_retraitmagasin_rem", Schema = "upways")]
[Index("Idcommande", Name = "idx_retraitmagasin_idcommande")]
[Index("Idinformations", Name = "idx_retraitmagasin_idinformations")]
[Index("Idmagasin", Name = "idx_retraitmagasin_idmagasin")]
public partial class Retraitmagasin
{
    [Key]
    [Column("rem_id")]
    public int RetraitMagasinId { get; set; }

    [Column("rem_idinformations")]
    public int?InformationId { get; set; }

    [Column("rem_idcommande")]
    public int? CommandeId { get; set; }

    [Column("rem_idmagasin")]
    public int MagasinId { get; set; }

    [Column("rem_date", TypeName = "date")]
    public DateOnly? DateRetrait { get; set; }

    [Column("rem_heure", TypeName = "heure")]
    public TimeOnly? HeureRetrait { get; set; }

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

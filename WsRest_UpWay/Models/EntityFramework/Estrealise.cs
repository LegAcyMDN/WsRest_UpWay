using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[PrimaryKey("Idvelo", "Idinspection", "Idreparation")]
[Table("t_j_estrealise_esr", Schema = "upways")]
[Index("Idinspection", Name = "idx_estrealise_idinspection")]
[Index("Idreparation", Name = "idx_estrealise_idreparation")]
[Index("Idvelo", Name = "idx_estrealise_idvelo")]
public partial class Estrealise
{
    [Key]
    [Column("esr_id")]
    public int VeloId { get; set; }

    [Key]
    [Column("esr_idInspection")]
    public int InspectionId { get; set; }

    [Key]
    [Column("esr_idReparation")]
    public int ReparationId { get; set; }

    [Column("esr_dateinspection", TypeName = "date")]
    public DateTime DateInspection { get; set; }

    [Column("esr_commentaireInspection", TypeName = "text")]
    [StringLength(4096)]
    public string? CommentaireInspection { get; set; }

    [Column("esr_historiqueInspection")]
    [StringLength(100)]
    public string? HistoriqueInspection { get; set; }

    [ForeignKey("Idinspection")]
    [InverseProperty("Estrealises")]
    public virtual Rapportinspection IdinspectionNavigation { get; set; } = null!;

    [ForeignKey("Idreparation")]
    [InverseProperty("Estrealises")]
    public virtual Reparationvelo IdreparationNavigation { get; set; } = null!;

    [ForeignKey("Idvelo")]
    [InverseProperty("Estrealises")]
    public virtual Velo IdveloNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_dpo_dpo", Schema = "upways")]
[Index("Idclient", Name = "idx_dpo_idclient")]
public partial class Dpo
{
    [Key]
    [Column("dpo_id")]
    public int DpoId { get; set; }

    [Column("clt_id")]
    public int? ClientId { get; set; }

    [Column("dpo_typope")]
    [StringLength(20)]
    public string? TypeOperation { get; set; }

    [Column("dpo_datreqdpo", TypeName = "char")]
    public DateTime? DateRequeteDpo { get; set; }
}

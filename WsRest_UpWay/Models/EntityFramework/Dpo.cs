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
    public int Iddpo { get; set; }

    [Column("clt_id")]
    public int? Idclient { get; set; }

    [Column("dpo_typope")]
    [StringLength(20)]
    public string? Typeoperation { get; set; }

    [Column("dpo_datreqdpo")]
    public DateOnly? Daterequetedpo { get; set; }
}

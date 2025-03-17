using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_photoaccessoire_pha", Schema = "upways")]
[Index(nameof(AccessoireId), Name = "ix_t_e_photoaccessoire_pha_accessoireid")]
public partial class PhotoAccessoire
{
    [Key]
    [Column("pha_id")]
    public int PhotoAcessoireId { get; set; }

    [Key]
    [Column("acs_id")]
    public int AccessoireId { get; set; }

    [Column("pha_url")]
    public byte[]? UrlPhotoAccessoire { get; set; }

    [ForeignKey(nameof(AccessoireId))]
    [InverseProperty(nameof(Accessoire.ListePhotoAccessoires))]
    public virtual Accessoire PhotoAccessoireAccessoire { get; set; } = null!;
}

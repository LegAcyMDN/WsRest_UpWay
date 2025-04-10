﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay.Models.EntityFramework;

[Table("t_e_moteur_mot", Schema = "upways")]
[Index(nameof(MarqueId), Name = "ix_t_e_moteur_mot_marqueid")]
public class Moteur : ISizedEntity
{
    public Moteur()
    {
        ListeVeloModifiers = new HashSet<VeloModifier>();
        ListeVelos = new HashSet<Velo>();
    }

    [Key] [Column("mot_id")] public int MoteurId { get; set; }

    [Column("mar_id")] public int MarqueId { get; set; }

    [Column("mot_modele")]
    [StringLength(50)]
    public string? ModeleMoteur { get; set; }

    [Column("mot_couple")]
    [StringLength(10)]
    public string? CoupleMoteur { get; set; }

    [Column("mot_vitessemax")]
    [StringLength(10)]
    public string? VitesseMaximal { get; set; }

    [ForeignKey(nameof(MarqueId))]
    [InverseProperty(nameof(Marque.ListeMoteurs))]
    public virtual Marque MoteurMarque { get; set; } = null!;

    [InverseProperty(nameof(VeloModifier.VeloModifierMoteur))]
    public virtual ICollection<VeloModifier> ListeVeloModifiers { get; set; } = new List<VeloModifier>();

    [InverseProperty(nameof(Velo.VeloMoteur))]
    public virtual ICollection<Velo> ListeVelos { get; set; } = new List<Velo>();

    public long GetSize()
    {
        return sizeof(int) * 2 + ModeleMoteur?.Length ?? 0 + CoupleMoteur?.Length ?? 0 + VitesseMaximal?.Length ?? 0;
    }
}
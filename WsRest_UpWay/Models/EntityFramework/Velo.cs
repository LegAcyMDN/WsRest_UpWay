using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework
{
    [Table("t_e_velo_vl")]
    public partial class Velo
    {
        [Key]
        [Column("vl_id")]
        public int VeloId { get; set; }


        [Column("vl_nom")]
        [StringLength(100)]
        public string VeloNom { get; set; }

        [Column("vl_annee")]
        [StringLength(4)]
        public int VeloAnnee { get; set; }

        [Column("vl_taille", TypeName = "char(15)")]
        [StringLength(15, ErrorMessage = "la taille comporte 15 caractères")]
        public string VeloTaille { get; set; }

        [Column("vl_nbkms")]
        [StringLength(5)]
        public int VeloNbKms { get; set; }

        [Column("vl_prixremise")]
        [StringLength(5)]
        public double VeloPrixRemise { get; set; }

        [Column("vl_prixneuf")]
        [StringLength(5)]
        public double VeloPrixNeuf { get; set; }

        [Column("vl_poucentagereduction")]
        [StringLength(3)]
        public int VeloPoucentageReduction { get; set; }

        [Column("vl_description")]
        [StringLength(5000)]
        public string VeloDescription { get; set; }

        [Column("vl_quantite")]
        [StringLength(3)]
        public string VeloQuantite { get; set; }

        [Column("vl_positionmoteur")]
        [StringLength(20)]
        public string VeloPositionMoteur { get; set; }
    }
}

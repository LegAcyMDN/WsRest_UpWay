using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework
{
    [Table("t_a_accessoire_acs")]

    public partial class Accessoire
    {
        [Key]
        [Column("acs_id")]
        public int IdAccessoire { get; set; }

        [Required]
        [Column("acs_nom")]
        [StringLength(100)]
        public string NomAccessoire { get; set; } = null!;

        [Required]
        [Column("acs_prix")]
        public decimal PrixAccessoire { get; set; } 

        [Column("acs_description")]
        [StringLength(1000)]
        public string DescriptionAccessoire { get; set; }


    }
}

using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsRest_UpWay.Models.EntityFramework
{
    [Table("t_c_utilisateur_cat")]
    public partial class Categorie
    {
        [Key]
        [Column("cat_id")]
        public int IdCategorie { get; set; }

        [Required]
        [Column("cat_libelle")]
        [StringLength(100)]
        public string LibelleCategorie { get; set; } = null!;
    }
}

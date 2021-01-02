using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("favorite")]
    public class Favorite
    {
        [Key]
        [Column("id_favorite")]
        public int IdFavorite { get; set; }

        [Column("title")]
        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        [Column("link")]
        [StringLength(255)]
        [Required]
        public string Link { get; set; }

        [Required]
        [Column("id_user")]
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; }
    }
}

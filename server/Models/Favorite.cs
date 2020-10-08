using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("favorite")]
    public class Favorite
    {
        [Key]
        [Column("id_favorite")]
        public int IdFavorite { get; set; }

        [Column("link")]
        [StringLength(255)]
        [Required]
        public string Link { get; set; }
    }
}

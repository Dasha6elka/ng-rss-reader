using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("user_x_favorite")]
    public class UserHasFavorite
    {
        [Required]
        [Column("id_user")]
        public int IdUser { get; set; }

        [Required]
        [Column("id_favorite")]
        public int IdFavorite { get; set; }

        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(IdFavorite))]
        public virtual Favorite Favorite { get; set; }
    }
}

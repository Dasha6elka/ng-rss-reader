using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("user_account")]
    public class User
    {
        [Key]
        [Column("id_user")]
        public int IdUser { get; set; }

        [Required]
        [Column("login")]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("channel")]
    public class Channel
    {
        [Key]
        [Column("id_channel")]
        public int IdChannel { get; set; }

        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Column("link")]
        [StringLength(255)]
        public string Link { get; set; }

        [Column("visible")]
        public bool Visible { get; set; }

        [Required]
        [Column("id_user")]
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; }
    }
}

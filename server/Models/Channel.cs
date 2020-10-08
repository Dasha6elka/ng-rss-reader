using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
    }
}

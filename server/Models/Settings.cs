using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("settings")]
    public class Settings
    {
        [Key]
        [Column("id_settings")]
        public int IdSettings { get; set; }

        [Required]
        [Column("dark_theme")]
        public bool DarkTheme { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("settings_x_channel")]
    public class SettingsHasChannel
    {
        [Required]
        [Column("id_channel")]
        public int IdChannel { get; set; }


        [Required]
        [Column("id_settings")]
        public int IdSettings { get; set; }

        [Required]
        [Column("visible")]
        public bool Visible { get; set; }

        [ForeignKey(nameof(IdChannel))]
        public virtual Channel Channel { get; set; }

        [ForeignKey(nameof(IdSettings))]
        public virtual Settings Settings { get; set; }
    }
}

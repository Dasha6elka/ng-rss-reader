using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server
{
    public class RssReaderContext : DbContext
    {
        public RssReaderContext(DbContextOptions<RssReaderContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.SettingsHasChannel>().HasKey(o => new { o.IdChannel, o.IdSettings });
            builder.Entity<Models.UserHasFavorite>().HasKey(o => new { o.IdFavorite, o.IdUser });
        }

        public DbSet<Models.Channel> Channels { get; set; }

        public DbSet<Models.Favorite> Favorites { get; set; }

        public DbSet<Models.Settings> Settings { get; set; }

        public DbSet<Models.SettingsHasChannel> SettingsHasChannels { get; set; }

        public DbSet<Models.User> Users { get; set; }

        public DbSet<Models.UserHasFavorite> UserHasFavorites { get; set; }
    }
}

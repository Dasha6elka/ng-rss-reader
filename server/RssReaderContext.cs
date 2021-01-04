using Microsoft.EntityFrameworkCore;

namespace server
{
    public class RssReaderContext : DbContext
    {
        public RssReaderContext(DbContextOptions<RssReaderContext> options) : base(options) { }

        public DbSet<Models.Channel> Channels { get; set; }

        public DbSet<Models.Favorite> Favorites { get; set; }

        public DbSet<Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>()
                .HasIndex(x => x.Login)
                .IsUnique();
        }
    }
}

namespace Feeder.Data.Context
{
    using Feeder.Data.Config;
    using Feeder.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class FeederContext : DbContext
    {
        public FeederContext(DbContextOptions<FeederContext> options) : base(options)
        {
        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CollectionConfig())
                .ApplyConfiguration(new FeedConfig());
        }
    }
}

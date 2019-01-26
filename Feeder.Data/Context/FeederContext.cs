namespace Feeder.Data.Context
{
    using Feeder.Core.Collection;
    using Feeder.Core.Feed;
    using Feeder.Core.User;
    using Feeder.Data.Config;
    using Microsoft.EntityFrameworkCore;

    public class FeederContext : DbContext
    {
        public FeederContext(DbContextOptions<FeederContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CollectionConfig())
                .ApplyConfiguration(new FeedConfig())
                .ApplyConfiguration(new UserConfig());
        }
    }
}

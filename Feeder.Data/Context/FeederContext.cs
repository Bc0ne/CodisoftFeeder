namespace Feeder.Data.Context
{
    using Feeder.Data.Config;
    using Feeder.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public class FeederContext : DbContext
    {
        public FeederContext(DbContextOptions<FeederContext> options) : base(options)
        {

        }

        public DbSet<Collection> Collections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CollectionConfig());
        }
    }
}

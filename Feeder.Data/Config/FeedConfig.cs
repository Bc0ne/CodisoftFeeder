namespace Feeder.Data.Config
{
    using Feeder.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    class FeedConfig : IEntityTypeConfiguration<Feed>
    {
        public void Configure(EntityTypeBuilder<Feed> builder)
        {
            builder
                 .Property(x => x.Id)
                 .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Link)
                    .IsRequired();

            builder
                .Property(x => x.Title)
                    .IsRequired();

            builder
                .HasOne(x => x.Collection)
                .WithMany(t => t.Feeds)
                .HasForeignKey(x => x.CollectionId);
        }
    }
}

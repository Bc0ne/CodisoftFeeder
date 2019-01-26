
namespace Feeder.Data.Config
{
    using Feeder.Core.User;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.FirstName)
                .IsRequired();

            builder
                .Property(x => x.LastName)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired();
        }
    }
}

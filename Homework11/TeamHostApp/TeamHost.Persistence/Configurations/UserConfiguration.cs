using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(i => i.RequestsSent)
            .WithOne(i => i.Sender)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(i => i.RequestsReceived)
            .WithOne(i => i.Receiver)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.MyFriends)
            .WithMany(i => i.FriendsWith);
    }
}
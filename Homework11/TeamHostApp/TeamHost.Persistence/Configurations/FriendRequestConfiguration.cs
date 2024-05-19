using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Persistence.Configurations;

public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        builder.Ignore(i => i.Id);
        
        builder.HasKey(i => new
        {
            i.SenderId,
            i.ReceiverId
        });
    }
}
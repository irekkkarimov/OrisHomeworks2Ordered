using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasMany(i => i.GamePurchases)
            .WithOne(i => i.Game)
            .HasForeignKey(i => i.GameId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
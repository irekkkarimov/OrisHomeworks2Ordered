using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities.WalletEntities;

namespace TeamHost.Persistence.Configurations;

public class GamePurchaseConfiguration : IEntityTypeConfiguration<GamePurchase>
{
    public void Configure(EntityTypeBuilder<GamePurchase> builder)
    {
        builder.HasKey(i => new { i.GameId, i.WalletId });
    }
}
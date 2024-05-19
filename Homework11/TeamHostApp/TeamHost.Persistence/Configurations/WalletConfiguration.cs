using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities.WalletEntities;

namespace TeamHost.Persistence.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasOne(i => i.User)
            .WithOne(i => i.Wallet)
            .HasForeignKey<Wallet>(i => i.UserId);
        
        builder.HasMany(i => i.GamePurchases)
            .WithOne(i => i.Wallet)
            .HasForeignKey(i => i.WalletId)
            .OnDelete(DeleteBehavior.SetNull);
        
        
    }
}
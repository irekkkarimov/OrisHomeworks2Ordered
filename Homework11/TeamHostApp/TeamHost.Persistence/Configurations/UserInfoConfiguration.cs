using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Persistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasOne(i => i.User)
            .WithOne(i => i.UserInfo)
            .HasForeignKey<UserInfo>(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Country)
            .WithMany(i => i.UserInfos)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
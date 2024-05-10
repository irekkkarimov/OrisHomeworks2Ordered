using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasOne(i => i.Country)
            .WithMany(i => i.Companies)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
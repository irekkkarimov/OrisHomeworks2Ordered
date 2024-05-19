using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHost.Domain.Entities;

namespace TeamHost.Persistence.Configurations;

public class StaticFileConfiguration : IEntityTypeConfiguration<StaticFile>
{
    public void Configure(EntityTypeBuilder<StaticFile> builder)
    {
        
    }
}
using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Configurations;

public class OperationClaimEntityTypeConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasData(new List<OperationClaim>
        {
            new (Guid.Parse("00000000-0000-0000-0000-000000000002"),Role.Admin),
            new (Guid.Parse("00000000-0000-0000-0000-000000000003"),Role.User)
        });

    }
}
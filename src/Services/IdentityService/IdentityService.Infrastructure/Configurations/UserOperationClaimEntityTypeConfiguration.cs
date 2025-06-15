using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Configurations;

public class UserOperationClaimEntityTypeConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.HasKey(uoc => new { uoc.UserId, uoc.OperationClaimId });
        
        builder.HasOne(uoc => uoc.User)
            .WithMany(user => user.UserOperationClaim)
            .HasForeignKey(uoc => uoc.UserId);

        builder.HasOne(uoc => uoc.OperationClaim)
            .WithMany(claim => claim.UserOperationClaim)
            .HasForeignKey(uoc => uoc.OperationClaimId);

        builder.HasData(new List<UserOperationClaim>()
        {
            new(Guid.Parse("00000000-0000-0000-0000-000000000001"),Guid.Parse("00000000-0000-0000-0000-000000000002")),
            new(Guid.Parse("00000000-0000-0000-0000-000000000001"),Guid.Parse("00000000-0000-0000-0000-000000000003")),
        });
    }
}
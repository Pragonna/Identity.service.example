using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserPortfolioService.Domain.Entities;

namespace UserPortfolioService.Infrastructure.Persistence.Configurations;

public class CryptoWalletEntityTypeConfiguration : IEntityTypeConfiguration<CryptoWalletEntity>
{
    public void Configure(EntityTypeBuilder<CryptoWalletEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.OwnsMany(c => c.Balances);
    }
}
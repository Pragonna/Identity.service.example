using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserPortfolioService.Domain.Entities;

namespace UserPortfolioService.Infrastructure.Persistence.Configurations;

public class UserPortfolioEntityTypeConfiguration : IEntityTypeConfiguration<UserPortfolioEntity>
{
    public void Configure(EntityTypeBuilder<UserPortfolioEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.DateOfBirth).IsRequired();

        builder.OwnsOne(u => u.Image);

        builder.HasOne(u => u.TelegramEntity)
            .WithOne(u => u.UserPortfolioEntity)
            .HasForeignKey<TelegramEntity>(u => u.UserPortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.TwitterEntity)
            .WithOne(u => u.UserPortfolioEntity)
            .HasForeignKey<TwitterEntity>(u => u.UserPortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.CryptoWallets)
            .WithOne(u => u.UserPortfolioEntity)
            .HasForeignKey(u => u.UserPortfolioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
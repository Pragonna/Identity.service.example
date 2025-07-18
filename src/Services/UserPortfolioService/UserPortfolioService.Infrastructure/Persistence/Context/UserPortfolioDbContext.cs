using IdentityService.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Common;
using UserPortfolioService.Domain.Entities;

namespace UserPortfolioService.Infrastructure.Persistence.Context;

public class UserPortfolioDbContext : DbContext
{
    public DbSet<UserPortfolioEntity> UserPortfolios { get; set; }
    public DbSet<TelegramEntity> Telegrams { get; set; }
    public DbSet<TwitterEntity> Twitters { get; set; }
    public DbSet<CryptoWalletEntity> CryptoWallets { get; set; }
    public UserPortfolioDbContext(DbContextOptions<UserPortfolioDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Entity && e.State == EntityState.Added || e.State == EntityState.Modified);
        foreach (var entry in entries)
        {
            var entity = entry.Entity as Entity;
            if (entry.State == EntityState.Added) entity.CreatedDate = DateTime.UtcNow;
            if (entry.State == EntityState.Modified) entity.ModifiedDate = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserPortfolioDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
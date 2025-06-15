using IdentityService.Domain.Entities;
using IdentityService.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.DataAccess.Common;

namespace IdentityService.Infrastructure.Context;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseUserEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));
        foreach (var entry in entries)
        {
            var entity = (BaseUserEntity)entry.Entity;
            if (entry.State == EntityState.Added) entity.CreatedDate = DateTime.UtcNow;
            if (entry.State == EntityState.Modified) entity.ModifiedDate = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
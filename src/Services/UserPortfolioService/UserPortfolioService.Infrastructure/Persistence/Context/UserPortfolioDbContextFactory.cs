using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserPortfolioService.Infrastructure.Persistence.Context;

public class UserPortfolioDbContextFactory : IDesignTimeDbContextFactory<UserPortfolioDbContext>
{
    public UserPortfolioDbContext CreateDbContext(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../UserPortfolioService.Api");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<UserPortfolioDbContext>();
        var connectionString = configuration.GetConnectionString("SqlConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'SqlConnection' not found.");
        }

        optionsBuilder.UseSqlServer(connectionString);

        return new UserPortfolioDbContext(optionsBuilder.Options);
    }
}
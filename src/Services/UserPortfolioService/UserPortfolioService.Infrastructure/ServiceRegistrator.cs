using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DataAccess.Repositories;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Infrastructure.Persistence.Context;
using UserPortfolioService.Infrastructure.External;
using UserPortfolioService.Infrastructure.Persistence.Repositories;

namespace UserPortfolioService.Infrastructure;

public static class ServiceRegistrator
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UserPortfolioDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });
       // services.AddSingleton<ICryptoPriceServer, CryptoPriceService>();
        services.AddScoped<IUserPortfolioRepository, UserPortfolioRepository>();
        services.AddScoped<IUserPortfolioUnitOfWork, UserPortfolioUnitOfWork>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IUserPortfolioUnitOfWork>());

        return services;
    }
}
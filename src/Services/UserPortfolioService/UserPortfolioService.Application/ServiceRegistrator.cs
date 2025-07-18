using Microsoft.Extensions.DependencyInjection;
using UserPortfolioService.UserPortfolio.Rules;

namespace UserPortfolioService;

public static class ServiceRegistrator
{
    public static IServiceCollection AddUserPortfolioService(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });
        services.AddScoped<UserPortfolioBusinessRules>();
        
        return services;
    }
}
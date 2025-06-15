using EventBus.EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.RabbitMQ;
using IdentityService.Application.Repositories;
using IdentityService.Infrastructure.Context;
using IdentityService.Infrastructure.Manager;
using IdentityService.Infrastructure.Repositories;
using IdentityService.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using IdentityService.Infrastructure.Validators;

namespace IdentityService.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IEventBus>(provider =>
        {
            return EventBusFactory.Create(
                RabbitMQOptions.GetConfigRabbitMQ(Directory.GetCurrentDirectory()), provider);
        });
        services.AddMemoryCache();
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });
        services.Configure<TokenOptions>(configuration.GetSection(nameof(TokenOptions)));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
        services.AddScoped<IUserAuthManager, UserAuthManager>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IInMemoryVerificationService, InMemoryVerificationService>();
        services.AddValidatorsFromAssemblies(new[]
        {
            typeof(UserRegisterValidator).Assembly,
            typeof(UserLoginValidator).Assembly
        });
        return services;
    }
}
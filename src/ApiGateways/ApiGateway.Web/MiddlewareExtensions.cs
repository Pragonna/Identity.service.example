using ApiGateway.Web.Middlewares;

namespace ApiGateway.Web;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddApiGatewayMiddleware(this IServiceCollection services)
    {
        services.AddTransient<ExceptionMiddleware>();
        services.AddTransient<RequestLoggingMiddleware>();
        return services;
    }
}
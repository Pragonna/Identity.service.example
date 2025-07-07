namespace ApiGateway.Web.Middlewares;

public class RequestLoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);
    
        await next(context);

        logger.LogInformation("Response status: {StatusCode}", context.Response.StatusCode);
    }
}
namespace ApiGateway.Web.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex,
                $"Unhandled exception occurred!\nException name: {nameof(ex)}\nException Message: {ex.Message}");
            
            await context.Response.WriteAsync($"An unexpected error occurred.\nStatus Code: {context.Response.StatusCode}");
        }
    }
}
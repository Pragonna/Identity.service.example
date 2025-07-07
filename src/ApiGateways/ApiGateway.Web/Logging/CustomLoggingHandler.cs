namespace ApiGateway.Web.Logging;

public class CustomLoggingHandler : DelegatingHandler
{
    private readonly ILogger<CustomLoggingHandler> _logger;

    public CustomLoggingHandler(ILogger<CustomLoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request to downstream: {Method} {Uri}", request.Method, request.RequestUri);
        
        var response = await base.SendAsync(request, cancellationToken);
        
        _logger.LogInformation("Response from downstream: {StatusCode}", response.StatusCode);
        return response;
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserPortfolioService.Api.Controllers.Common;

public class BaseController(IMediator mediator) : ControllerBase
{
    protected IMediator _mediator => mediator??HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

    private string? GetIpAddress()
    {
        // Check if the request contains the "X-Forwarded-For" header.
        // This header is used when the request goes through a proxy or load balancer.
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];

        // If "X-Forwarded-For" is not found, get the remote IP address of the client.
        // Convert IPv6 to IPv4 if needed and return it as a string.
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }
}
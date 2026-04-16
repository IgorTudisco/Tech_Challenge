using CloudGame.Domain.Commom;
using Microsoft.AspNetCore.Diagnostics;

namespace CloudGame.API.Handlers;

public sealed class CloudGameExceptionHandler(ILogger<CloudGameExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<CloudGameExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred while processing the request.");

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsync(new Error("Exception", $"{exception.Message} - traceid:{httpContext.TraceIdentifier}").ToString(), cancellationToken);
        return true;
    }
}

using CSharpFunctionalExtensions;

namespace AdvertApp.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex.Message);
            logger.LogError(ex.StackTrace);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            logger.LogError(ex.StackTrace);
            throw;
        }
    }
}
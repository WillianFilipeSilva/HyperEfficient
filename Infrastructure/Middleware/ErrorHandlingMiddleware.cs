namespace HyperEfficient.Infrastructure.Middleware;

public class ErrorHandlingMiddleware
{
    private const string MensagemPadrao = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.";
    private readonly ILogger<ErrorHandlingMiddleware> _log;
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> log)
    {
        _next = next;
        _log = log;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Exception não tratada ");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = MensagemPadrao,
                details = ex.Message
            });
        }
    }
}
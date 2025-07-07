using System.Net;
using System.Text.Json;

namespace HyperEfficient.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";

            ErrorResponse errorResponse = exception switch
            {
                InvalidCredentialsException => new ErrorResponse { StatusCode = (int)HttpStatusCode.Unauthorized, Message = exception.Message, Details = "Credenciais inválidas" },
                KeyNotFoundException => new ErrorResponse { StatusCode = (int)HttpStatusCode.NotFound, Message = exception.Message, Details = "Recurso não encontrado" },
                ArgumentException => new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = exception.Message, Details = "Parâmetros inválidos" },
                UnauthorizedAccessException => new ErrorResponse { StatusCode = (int)HttpStatusCode.Unauthorized, Message = "Acesso não autorizado", Details = "Token inválido ou expirado" },
                InvalidOperationException => new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = exception.Message, Details = "Operação inválida" },
                _ => new ErrorResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Ocorreu um erro interno no servidor", Details = "Tente novamente mais tarde" }
            };

            _log.LogError($"Exception: {exception.Message} | StatusCode: {errorResponse.StatusCode}\nStackTrace: {exception.StackTrace}");

            response.StatusCode = errorResponse.StatusCode;

            string jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            await response.WriteAsync(jsonResponse);
        }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message) : base(message) { }
    }
}
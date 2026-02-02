using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
        {
            // Recuperamos el TraceId que inyectamos en TraceIdMiddleware
            var traceId = context.Items["TraceId"]?.ToString();

            // Determinamos el código de estado y el mensaje
            var (statusCode, message) = exception switch
            {
                FluentValidation.ValidationException ve => (StatusCodes.Status400BadRequest, "Error de validación de datos."),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "El recurso solicitado no existe."),
                BusinessRuleException bre => (StatusCodes.Status409Conflict, bre.Message), // O tu excepción personalizada
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "No autorizado."),
                _ => (StatusCodes.Status500InternalServerError, "Ha ocurrido un error inesperado en el servidor.")
            };

            var response = new
            {
                TraceId = traceId,
                IsSuccess = false,
                Response = (object?)null,
                ErrorMessages = exception is FluentValidation.ValidationException vEx
                    ? vEx.Errors.Select(e => e.ErrorMessage).ToList()
                    : new List<string> { exception.Message }
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(response, ct);

            return true;
        }
    }
}

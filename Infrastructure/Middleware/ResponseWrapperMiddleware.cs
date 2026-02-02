
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Infrastructure.Middleware
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            try
            {
                await _next(context);
                // Si todo sale bien, procesamos y envolvemos
                await WrapResponseAsync(context, memoryStream, originalBodyStream);
            }
            finally
            {
                // AJUSTE VITAL: Restauramos el stream original pase lo que pase.
                // Si hubo un error, esto permite que GlobalExceptionHandler 
                // pueda escribir en el socket real del cliente.
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task WrapResponseAsync(HttpContext context, MemoryStream memoryStream, Stream originalStream)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(memoryStream).ReadToEndAsync();

            if (ShouldWrap(context, bodyText))
            {
                var apiResponse = new
                {
                    TraceId = context.Items["TraceId"],
                    IsSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                    Response = JsonSerializer.Deserialize<object>(bodyText)
                };

                // Cambiamos el stream de vuelta antes de escribir la respuesta final
                context.Response.Body = originalStream;
                context.Response.ContentType = "application/json";

                // Ahora sí escribimos al stream real que va al cliente
                await context.Response.WriteAsJsonAsync(apiResponse);
            }
            else
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalStream);
            }
        }

        private bool ShouldWrap(HttpContext context, string body) =>
            !body.Contains("\"traceId\"") && !string.IsNullOrWhiteSpace(body);
    }
}

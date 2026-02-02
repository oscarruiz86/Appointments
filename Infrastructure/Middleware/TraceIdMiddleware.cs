
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Infrastructure.Middleware
{
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string traceId = context.Request.Headers["TraceId"].FirstOrDefault()
                             ?? Guid.NewGuid().ToString();

            // Sincronizamos con el estándar de .NET
            context.TraceIdentifier = traceId;
            context.Items["TraceId"] = traceId;

            // Usamos el nombre estándar "TraceId" para evitar duplicidad con @tr
            using (LogContext.PushProperty("TraceId", traceId))
            using (LogContext.PushProperty("Endpoint", context.Request.Path))
            using (LogContext.PushProperty("Method", context.Request.Method))
            {
                context.Response.Headers["TraceId"] = traceId;
                await _next(context);
            }
        }
    }
}

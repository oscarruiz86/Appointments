using Microsoft.AspNetCore.Http;

namespace Infrastructure.Tenancy;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITenantProvider tenantProvider)
    {
        Guid? tenantId = null;

        // JWT
        var claim = context.User.FindFirst("tenantId");
        if (claim != null)
            tenantId = Guid.Parse(claim.Value);

        // Header (testing/swagger)
        else if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var header))
            tenantId = Guid.Parse(header!);

        tenantProvider.TenantId = tenantId;

        await _next(context);
    }
}

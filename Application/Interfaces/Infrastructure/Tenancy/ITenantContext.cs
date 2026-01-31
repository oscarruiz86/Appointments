using Domain.Entities;

namespace Application.Interfaces.Infrastructure.Tenancy
{
    public interface ITenantContext
    {
        Guid? TenantId { get; }
        Tenant? Tenant { get; }

        void SetTenant(Tenant tenant);
    }
}

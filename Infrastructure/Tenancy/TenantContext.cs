using Application.Interfaces.Infrastructure.Tenancy;
using Domain.Entities;

namespace Infrastructure.Tenancy
{
    public class TenantContext : ITenantContext
    {
        private readonly ITenantProvider _tenantProvider;

        public TenantContext(ITenantProvider tenantProvider)
        {
            _tenantProvider = tenantProvider;
        }

        public Guid? TenantId =>
            _tenantProvider.TenantId == Guid.Empty
                ? null
                : _tenantProvider.TenantId;

        public Tenant? Tenant { get; private set; }

        public void SetTenant(Tenant tenant)
        {
            Tenant = tenant;
        }
    }
}

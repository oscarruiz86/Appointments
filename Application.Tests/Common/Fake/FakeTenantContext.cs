using Application.Interfaces.Infrastructure.Tenancy;
using Domain.Entities;

namespace Application.Tests.Common.Fake
{
    public class FakeTenantContext : ITenantContext
    {
        public Guid? TenantId { get; private set; }
        public Tenant? Tenant { get; private set; }

        public FakeTenantContext()
        {
            TenantId = FakeIds.TenantId;
        }

        public void SetTenant(Tenant tenant)
        {
            Tenant = tenant;
            TenantId = tenant.Id;
        }
    }
}

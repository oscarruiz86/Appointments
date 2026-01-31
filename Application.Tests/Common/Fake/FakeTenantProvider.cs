using Infrastructure.Tenancy;

namespace Application.Tests.Common.Fake
{
    public class FakeTenantProvider : ITenantProvider
    {
        public Guid? TenantId { get; set; } = Guid.NewGuid();
    }
}

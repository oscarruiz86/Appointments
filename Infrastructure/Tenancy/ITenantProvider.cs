

namespace Infrastructure.Tenancy
{
    public interface ITenantProvider
    {
        Guid? TenantId { get; set; }
    }
}

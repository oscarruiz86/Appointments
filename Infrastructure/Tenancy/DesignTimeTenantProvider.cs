
namespace Infrastructure.Tenancy
{
    public class DesignTimeTenantProvider : ITenantProvider
    {
        public Guid? TenantId { get; set; } = Guid.Empty;
    }
}

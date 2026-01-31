
namespace Infrastructure.Tenancy
{
    public class TenantProvider : ITenantProvider
    {
        public Guid? TenantId { get; set; }
    }
}

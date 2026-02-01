
namespace Domain.Entities.Common
{
    public interface ITenantScoped
    {
        Guid TenantId { get; }
    }
}

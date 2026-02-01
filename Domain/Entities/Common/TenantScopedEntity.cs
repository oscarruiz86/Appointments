namespace Domain.Entities.Common
{
    public abstract class TenantScopedEntity : ITenantScoped
    {
        public Guid TenantId { get; set; }
    }
}

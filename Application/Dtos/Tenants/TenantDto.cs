
namespace Application.Dtos.Tenants
{
    public class TenantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}

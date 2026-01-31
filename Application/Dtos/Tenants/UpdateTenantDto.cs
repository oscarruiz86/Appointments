
namespace Application.Dtos.Tenants
{
    public class UpdateTenantDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;
    }
}


namespace Application.Dtos.Auth
{
    public class AuthTenantDto
    {
        public Guid TenantId { get; set; }
        public string TenantName { get; set; } = string.Empty;
    }
}

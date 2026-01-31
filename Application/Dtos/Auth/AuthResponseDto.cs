

namespace Application.Dtos.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}

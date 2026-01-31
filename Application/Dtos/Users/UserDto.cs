using Application.Dtos.Roles;

namespace Application.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public List<RoleDto> Roles { get; set; } = [];
    }
}

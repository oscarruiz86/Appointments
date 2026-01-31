
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid TenantId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public Tenant Tenant { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}

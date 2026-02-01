
using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, ITenantScoped, IAuditableEntity
    {
        public Guid TenantId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public Tenant Tenant { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // Auditoría
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

    }
}

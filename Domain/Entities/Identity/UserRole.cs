

namespace Domain.Entities.Identity
{
    public class UserRole : EntityBase
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public Guid RoleId { get; set; }
        public ApplicationRole Role { get; set; } = null!;
    }
}

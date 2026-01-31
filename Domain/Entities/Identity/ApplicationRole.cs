

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
 
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}

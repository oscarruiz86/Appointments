using Domain.Entities.Common;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public Guid? UserId { get; set; }

        public string DisplayName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public bool Active { get; set; }

        public required ApplicationUser User { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}

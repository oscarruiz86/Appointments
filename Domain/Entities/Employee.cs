using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Employee : EntityBase
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Color { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ApplicationUser? User { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}

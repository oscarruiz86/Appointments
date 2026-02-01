

using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Tenant: IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ICollection<Employee>? Employees { get; set; }

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

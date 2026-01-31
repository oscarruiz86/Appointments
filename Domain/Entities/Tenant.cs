
namespace Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}

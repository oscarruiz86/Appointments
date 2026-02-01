


namespace Domain.Entities
{
    public class Customer : EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}

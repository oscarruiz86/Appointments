


namespace Domain.Entities
{
    public class Appointment : EntityBase
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public Guid StatusId { get; set; } 

        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
        public Service? Service { get; set; }
        public AppointmentStatus? Status { get; set; }
    }
}

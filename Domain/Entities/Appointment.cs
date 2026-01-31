

using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public Guid StatusId { get; set; } 

        public required Customer Customer { get; set; }
        public required Employee Employee { get; set; }
        public required Service Service { get; set; }
        public required AppointmentStatus Status { get; set; }
    }
}

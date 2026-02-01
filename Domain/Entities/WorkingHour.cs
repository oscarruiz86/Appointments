
namespace Domain.Entities
{
    public class WorkingHour : EntityBase
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }

        // 0 = Sunday | 6 = Saturday
        public int Weekday { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public required Employee Employee { get; set; }
    }
}

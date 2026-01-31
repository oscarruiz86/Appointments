
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class WorkingHour : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        // 0 = Sunday | 6 = Saturday
        public int Weekday { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public required Employee Employee { get; set; }
    }
}

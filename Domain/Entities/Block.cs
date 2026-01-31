

using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Block : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public string Reason { get; set; } = string.Empty;

        public required Employee Employee { get; set; }
    }
}

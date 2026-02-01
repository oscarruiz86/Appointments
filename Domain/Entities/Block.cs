
namespace Domain.Entities
{
    public class Block : EntityBase
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public string Reason { get; set; } = string.Empty;

        public required Employee Employee { get; set; }
    }
}

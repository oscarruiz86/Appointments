
namespace Domain.Entities
{
    public class Service : EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}

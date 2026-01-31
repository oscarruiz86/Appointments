
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}

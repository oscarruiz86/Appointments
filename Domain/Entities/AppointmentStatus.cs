

using Domain.Entities.Common;

namespace Domain.Entities
{
    public class AppointmentStatus : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public bool BlocksSlot { get; set; }     // ocupa horario
        public bool IsFinal { get; set; }        // terminado/cancelado
        public bool CountsAsSale { get; set; }   // métricas ventas

        public ICollection<Appointment>? Appointments { get; set; }
    }
}

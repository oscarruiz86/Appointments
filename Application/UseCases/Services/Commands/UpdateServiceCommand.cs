
using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Services.Commands
{
    public class UpdateServiceCommand : IRequest, IRequireActiveTenant
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

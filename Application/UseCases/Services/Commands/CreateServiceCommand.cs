using Application.Common.Tenancy;
using Application.Dtos.Services;
using MediatR;

namespace Application.UseCases.Services.Commands
{
    public class CreateServiceCommand : IRequest<CreateServiceResponseDto>, IRequireActiveTenant
    {
        public string Name {get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public decimal Price { get; set; }
    }
}

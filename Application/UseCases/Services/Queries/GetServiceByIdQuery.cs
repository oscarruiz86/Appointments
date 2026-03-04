using Application.Common.Tenancy;
using Application.Dtos.Services;
using MediatR;

namespace Application.UseCases.Services.Queries
{
    public class GetServiceByIdQuery : IRequest<ServiceDto>, IRequireActiveTenant
    {
        public Guid ServiceId { get; set; }
    }
}

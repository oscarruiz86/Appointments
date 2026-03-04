
using Application.Common.Tenancy;
using Application.Dtos.Services;
using MediatR;

namespace Application.UseCases.Services.Queries
{
    public class ListServicesQuery : IRequest<List<ServiceDto>>, IRequireActiveTenant
    {
        public bool OnlyActive { get; set; } = true;
    }
}

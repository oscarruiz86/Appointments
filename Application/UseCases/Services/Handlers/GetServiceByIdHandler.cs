
using Application.Dtos.Services;
using Application.Interfaces.Persistence;
using Application.UseCases.Services.Mappers;
using Application.UseCases.Services.Queries;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Services.Handlers
{
    public class GetServiceByIdHandler : IRequestHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly IUnitOfWork _uow;

        public GetServiceByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ServiceDto> Handle(GetServiceByIdQuery query, CancellationToken ct)
        {
            var repoUser = _uow.Repository<Service, Guid>();
            var service = await repoUser.GetByIdOrThrow(query.ServiceId);
            return service.ToDto();
        }
    }
}

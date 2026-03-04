using Application.Dtos.Services;
using Application.Interfaces.Persistence;
using Application.UseCases.Services.Mappers;
using Application.UseCases.Services.Queries;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Services.Handlers
{
    public class ListServicesHandler : IRequestHandler<ListServicesQuery, List<ServiceDto>>
    {

        private readonly IUnitOfWork _uow;

        public ListServicesHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<List<ServiceDto>> Handle(ListServicesQuery query, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<Service, Guid>();

            var services = repo.Query();

            if (query.OnlyActive)
                services = services.Where(x => x.IsActive);

            await Task.Delay(1000);

            return services.ToDtoList();
        }
    }
}

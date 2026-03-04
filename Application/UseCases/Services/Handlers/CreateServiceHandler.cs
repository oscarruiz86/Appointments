
using Application.Dtos.Services;
using Application.Interfaces.Persistence;
using Application.UseCases.Services.Commands;
using Application.UseCases.Services.Mappers;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Services.Handlers
{
    public class CreateServiceHandler : IRequestHandler<CreateServiceCommand, CreateServiceResponseDto>
    {

        private readonly IUnitOfWork _uow;

        public CreateServiceHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CreateServiceResponseDto> Handle(CreateServiceCommand request, CancellationToken ct)
        {
            var repository = _uow.Repository<Service,Guid>();
            var service = request.ToEntity();
            await repository.Add(service);
            await _uow.SaveChangesAsync();
            return service.ToCreateResponse();
        }
    }
}


using Application.Interfaces.Persistence;
using Application.UseCases.Services.Commands;
using Application.UseCases.Services.Mappers;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Services.Handlers
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateServiceHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task Handle(UpdateServiceCommand request, CancellationToken ct)
        {
            var repoUser = _uow.Repository<Service, Guid>();
            var service = await repoUser.GetByIdOrThrow(request.ServiceId);
            request.ToEntity(service);
            await repoUser.Update(service);
            await _uow.SaveChangesAsync(ct);
        }
    }
}

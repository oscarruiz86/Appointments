
using Application.Interfaces.Persistence;
using Application.UseCases.Services.Commands;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Services.Handlers
{
    public class DisableServiceHandler : IRequestHandler<DisableServiceCommand>
    {
        private readonly IUnitOfWork _uow;

        public DisableServiceHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task Handle(DisableServiceCommand request, CancellationToken ct)
        {
            var repoUser = _uow.Repository<Service, Guid>();
            var service = await repoUser.GetByIdOrThrow(request.ServiceId);

            service.IsActive = false;
            await repoUser.Update(service);
            await _uow.SaveChangesAsync(ct);
        }
    }
}


using Application.Common.Rules;
using Application.Interfaces.Persistence;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Mappers;
using Domain.Entities.Identity;
using MediatR;

namespace Application.UseCases.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;

        public UpdateUserHandler(
            RuleFactory ruleFactory,
            IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken ct)
        {
            var repo = _uow.Repository<ApplicationUser, Guid>();

            var user = await repo.GetByIdOrThrow(request.UserId);

            await RuleChecker.CheckAsync(
                   _ruleFactory.CanManageUser(user.TenantId)
               );

            request.ToEntity(user);

            await repo.Update(user);
            await _uow.SaveChangesAsync();
        }
    }

}

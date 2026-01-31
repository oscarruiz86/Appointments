

using Application.Common.Rules;
using Application.Interfaces.Persistence;
using Application.UseCases.Users.Commands;
using Domain.Entities.Identity;
using MediatR;

namespace Application.UseCases.Users.Handlers
{
   public class DisableUserHandler : IRequestHandler<DisableUserCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;

        public DisableUserHandler(
            RuleFactory ruleFactory,
            IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
        }

        public async Task Handle(DisableUserCommand request, CancellationToken ct)
        {
            var repo = _uow.Repository<ApplicationUser, Guid>();

            var user = await repo.GetByIdOrThrow(request.UserId);

            await RuleChecker.CheckAsync(
                _ruleFactory.UserMustExist(user),
                _ruleFactory.UserIsActive(user),
                _ruleFactory.CannotDisableSelf(request.UserId),
                _ruleFactory.CanManageUser(user.TenantId) 
            );

            user.IsActive = false;

            await repo.Update(user);
            await _uow.SaveChangesAsync();
        }
    }

}

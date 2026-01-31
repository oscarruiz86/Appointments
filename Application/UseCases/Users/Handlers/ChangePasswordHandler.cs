
using Application.Common.Rules;
using Application.Interfaces.Persistence;
using Application.UseCases.Users.Commands;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Users.Handlers
{
   public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly RuleFactory _ruleFactory;

        public ChangePasswordHandler(
            IUnitOfWork uow,
            RuleFactory ruleFactory,
            IPasswordHasher<ApplicationUser> hasher)
        {
            _uow = uow;
            _hasher = hasher;
            _ruleFactory = ruleFactory;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken ct)
        {
            var repo = _uow.Repository<ApplicationUser, Guid>();

            var user = await repo.GetByIdOrThrow(request.UserId);

            await RuleChecker.CheckAsync(
                             _ruleFactory.UserMustExist(user),
                             _ruleFactory.UserIsActive(user),
                             _ruleFactory.ChangePasswordPermission(user),
                             _ruleFactory.CanManageUser(user.TenantId),
                             _ruleFactory.PasswordNotSame(user, request.NewPassword)
                           );

            user.PasswordHash = _hasher.HashPassword(user, request.NewPassword);

            await repo.Update(user);
            await _uow.SaveChangesAsync();
        }
    }

}

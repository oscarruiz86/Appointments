

using Application.Common.Rules;
using Application.Dtos.Users;
using Application.Interfaces.Persistence;
using Application.UseCases.Users.Mappers;
using Application.UseCases.Users.Queries;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Users.Handlers
{
    public class GetUserByEmailHandler: IRequestHandler<GetByUserEmailQuery, UserDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;

        public GetUserByEmailHandler(
           RuleFactory ruleFactory,
           IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
        }

        public async Task<UserDto?> Handle(GetByUserEmailQuery query, CancellationToken ct)
        {
            var repoUser = _uow.Repository<ApplicationUser, Guid>();
            var repoUserRol = _uow.Repository<UserRole, Guid>();

            var user = await repoUser.Query().FirstOrDefaultAsync(x => x.Email == query.Email);
            if (user == null)
                return null;

            await RuleChecker.CheckAsync(
                    _ruleFactory.UserIsActive(user)
                );

            user.UserRoles = repoUserRol.Query()
                .Include(ur => ur.Role)
                .Where(ur => ur.User.Email == user.Email)
                .ToList();

            return user.ToUser();
        }
    }
}

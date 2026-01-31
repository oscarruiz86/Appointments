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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;

        public GetUserByIdHandler(
            RuleFactory ruleFactory,
            IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken ct)
        {
            var repoUser = _uow.Repository<ApplicationUser, Guid>();
            var repoUserRol = _uow.Repository<UserRole, Guid>();

            var user = await repoUser.GetByIdOrThrow(query.UserId);

            await RuleChecker.CheckAsync(
                    _ruleFactory.UserIsActive(user),
                    _ruleFactory.CanViewUser(user)
                );

            user.UserRoles = repoUserRol.Query()
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == user.Id)
                .ToList();

            return user.ToUser();
        }
    }

}

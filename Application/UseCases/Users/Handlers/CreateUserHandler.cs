

using MediatR;
using Application.Common.Rules;
using Application.Dtos.Users;
using Application.Interfaces.Persistence;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Mappers;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces.Infrastructure.Tenancy;

namespace Application.UseCases.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand,CreateUserResponseDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly RuleFactory _ruleFactory;
        private readonly ITenantContext _tenantContext;

        public CreateUserHandler(
            IUnitOfWork uow,
            RuleFactory ruleFactory,
            ITenantContext tenantContext,
            IPasswordHasher<ApplicationUser> hasher)
        {
            _uow = uow;
            _hasher = hasher;
            _ruleFactory = ruleFactory;
            _tenantContext = tenantContext;
        }

        public async Task<CreateUserResponseDto> Handle(CreateUserCommand request, CancellationToken ct)
        {

            var tenantId = _tenantContext.TenantId!.Value;
            var user = request.ToEntity(tenantId);

            await RuleChecker.CheckAsync(
                   _ruleFactory.CanManageUser(tenantId),
                   _ruleFactory.EmailUnique(user),
                   _ruleFactory.RolesMustExist(request.RoleIds)
               );

            var userRepo = _uow.Repository<ApplicationUser, Guid>();
            var userRoleRepo = _uow.Repository<UserRole, Guid>();            

            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            await userRepo.Add(user);

            foreach (var role in request.RoleIds)
            {
                await userRoleRepo.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = role,
                    TenantId = tenantId
                });
            }

            await _uow.SaveChangesAsync();

            return user.ToCreateResponse();
        }
    }
}

using Application.Common.Rules;
using Application.Common.Rules.Security;
using Application.Dtos.Auth;
using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Persistence;
using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Mappers;
using Domain.Entities;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Auth.Handlers
{
    public class ImpersonateHandler  : IRequestHandler<ImpersonateCommand,AuthResponseDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;
        private readonly AdminRule _adminRule;
        private readonly RuleFactory _ruleFactory;
        private readonly IJwtTokenGenerator _jwt;

        public ImpersonateHandler(
            IUnitOfWork uow,
            ICurrentUserService currentUser,
            AdminRule adminRule,
            RuleFactory ruleFactory,
            IJwtTokenGenerator jwt)
        {
            _uow = uow;
            _currentUser = currentUser;
            _adminRule = adminRule;
            _ruleFactory = ruleFactory;
            _jwt = jwt;
        }

        public async Task<AuthResponseDto> Handle(ImpersonateCommand tenant, CancellationToken ct)
        {
            var repoUser = _uow.Repository<ApplicationUser, Guid>();
            var repoRole = _uow.Repository<UserRole, Guid>();
            var repoTenant = _uow.Repository<Tenant, Guid>();

            var user = await repoUser.GetByIdOrThrow(_currentUser.UserId);
            var tenantUser = await repoTenant.GetByIdOrThrow(tenant.TenantId);


            await RuleChecker.CheckAsync(
                    _adminRule,
                    _ruleFactory.UserIsActive(user),
                    _ruleFactory.CanViewUser(user)
                );

            user.UserRoles = await repoRole.Query()
                .Include(ur=> ur.Role)
                .Where(x=> x.Role.Name == "TenantAdmin")
                .Take(1)
                .ToListAsync();

            user.TenantId = tenant.TenantId;
            var token = _jwt.Generate(user);

            return user.ToAuthResponse(token);

        }
    }
}


using Application.Common.Rules;
using Application.Dtos.Auth;
using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Persistence;
using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Mappers;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Persistence.Filters;

namespace Application.UseCases.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly IJwtTokenGenerator _jwt;
        private readonly RuleFactory _ruleFactory;
        private readonly IFilterContext _filterContext;

        public LoginHandler(
            IUnitOfWork uow,
            IPasswordHasher<ApplicationUser> hasher,
            RuleFactory ruleFactory,
            IFilterContext filterContext,
            IJwtTokenGenerator jwt)
        {
            _uow = uow;
            _hasher = hasher;
            _jwt = jwt;
            _ruleFactory = ruleFactory;
            _filterContext= filterContext;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken ct)
        {

            _filterContext.DisableTenantFilter = true;

            var repo = _uow.Repository<ApplicationUser, Guid>();

            var user = await repo.Query()
                            .Include(x => x.Tenant)
                            .Include(x => x.UserRoles!)
                                .ThenInclude(ur => ur.Role)
                            .FirstOrDefaultAsync(x =>
                                x.Email == request.Email &&
                                x.TenantId == request.TenantId &&
                                x.IsActive) ?? throw new UnauthorizedAccessException("Credenciales inválidas");


            await RuleChecker.CheckAsync(
                            _ruleFactory.PasswordMustBeValid(user!, request.Password),
                            _ruleFactory.UserMustHaveRoles(user!),
                            _ruleFactory.UserMustHaveTenant(user!) 
                          );

            var token = _jwt.Generate(user);

            return user.ToAuthResponse(token);
        }
    }
}

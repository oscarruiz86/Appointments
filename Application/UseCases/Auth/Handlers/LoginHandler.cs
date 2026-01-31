
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

namespace Application.UseCases.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly IJwtTokenGenerator _jwt;
        private readonly RuleFactory _ruleFactory;

        public LoginHandler(
            IUnitOfWork uow,
            IPasswordHasher<ApplicationUser> hasher,
            RuleFactory ruleFactory,
            IJwtTokenGenerator jwt)
        {
            _uow = uow;
            _hasher = hasher;
            _jwt = jwt;
            _ruleFactory = ruleFactory;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken ct)
        {
            var repo = _uow.Repository<ApplicationUser, Guid>();

            var user = await repo.Query()
                            .IgnoreQueryFilters()
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

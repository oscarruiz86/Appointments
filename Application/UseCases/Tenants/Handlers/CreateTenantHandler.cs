using Application.Common.Rules;
using Application.Common.Rules.Security;
using Application.Dtos.Tenants;
using Application.Interfaces.Persistence;
using Application.UseCases.Tenants.Commands;
using Application.UseCases.Tenants.Mappers;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tenants.Handlers
{
    public class CreateTenantHandler :IRequestHandler<CreateTenantCommand, CreateTenantResponseDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly AdminRule _adminRule;
        private readonly RuleFactory _ruleFactory;

        public CreateTenantHandler(
            AdminRule adminRule,
            RuleFactory ruleFactory,
            IUnitOfWork uow
            )
        {
            _uow = uow;
            _adminRule = adminRule;
            _ruleFactory = ruleFactory;
        }

        public async Task<CreateTenantResponseDto> Handle(CreateTenantCommand request, CancellationToken ct)
        {
            

            var repo = _uow.Repository<Tenant, Guid>();

            var tenant = request.ToEntity();

            await RuleChecker.CheckAsync(
                            _adminRule,
                            _ruleFactory.TenantNameUnique(tenant),
                            _ruleFactory.TenantPhoneUnique(tenant)
                          );

            await repo.Add(tenant);

            await _uow.SaveChangesAsync();

            return tenant.ToCreateResponse();
        }
    }
}

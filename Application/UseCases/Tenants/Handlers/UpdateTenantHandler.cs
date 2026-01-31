
using Application.Common.Rules;
using Application.Common.Rules.Security;
using Application.Interfaces.Persistence;
using Application.UseCases.Tenants.Commands;
using Application.UseCases.Tenants.Mappers;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tenants.Handlers
{
    public class UpdateTenantHandler : IRequestHandler<UpdateTenantCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;
        private readonly AdminRule _adminRule;

        public UpdateTenantHandler(
            IUnitOfWork uow,
            RuleFactory ruleFactory,
            AdminRule adminRule
            )
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
            _adminRule = adminRule;
        }

        public async Task Handle(UpdateTenantCommand request, CancellationToken ct)
        {

           

            var repo = _uow.Repository<Tenant, Guid>();

            var tenant = await repo.GetByIdOrThrow(request.Id);

            await RuleChecker.CheckAsync(
                            _adminRule,
                            _ruleFactory.TenantMustExist(request.Id),
                            _ruleFactory.TenantNameUnique(tenant),
                            _ruleFactory.TenantPhoneUnique(tenant)
                          );

            request.MapToEntity(tenant);

            await repo.Update(tenant);

            await _uow.SaveChangesAsync();
        }
    }
}

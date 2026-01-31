using Application.Common.Rules;
using Application.Common.Rules.Security;
using Application.Interfaces.Persistence;
using Application.UseCases.Tenants.Commands;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tenants.Handlers
{
    public class DisableTenantHandler : IRequestHandler<DisableTenantCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;
        private readonly AdminRule _adminRule;

        public DisableTenantHandler(
            IUnitOfWork uow,
            RuleFactory ruleFactory,
            AdminRule adminRule
            )
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
            _adminRule = adminRule;
        }

        public async Task Handle(DisableTenantCommand request, CancellationToken ct)
        {
            var repo = _uow.Repository<Tenant, Guid>();

            await RuleChecker.CheckAsync(
                             _adminRule,
                             _ruleFactory.TenantMustExist(request.Id)
                           );
            var tenant = await repo.GetByIdOrThrow(request.Id);
            tenant.IsActive = false;

            await _uow.SaveChangesAsync();
        }
    }
}

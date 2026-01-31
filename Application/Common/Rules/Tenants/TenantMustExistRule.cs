using Application.Interfaces.Persistence;
using Domain.Entities;

namespace Application.Common.Rules.Tenants
{
   public class TenantMustExistRule : IRule
{
    private readonly IUnitOfWork _uow;
    private readonly Guid _tenantId;

    public TenantMustExistRule(
        IUnitOfWork uow,
        Guid tenantId)
    {
        _uow = uow;
        _tenantId = tenantId;
    }

    public async Task<RuleResult> CheckAsync()
    {
        var repo = _uow.Repository<Tenant, Guid>();

        var exists = await repo.ExistsAsync(t => t.Id == _tenantId);

        if (!exists)
            return RuleResult.Fail("El tenant no existe");

        return RuleResult.Ok();
    }
}
}

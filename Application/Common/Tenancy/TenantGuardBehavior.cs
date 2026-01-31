
using MediatR;
using Application.Interfaces.Persistence;
using Domain.Entities;
using Application.Interfaces.Infrastructure.Tenancy;

namespace Application.Common.Tenancy
{
    public class TenantGuardBehavior<TRequest, TResponse>
      : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IUnitOfWork _uow;

        public TenantGuardBehavior(
            ITenantContext tenantContext,
            IUnitOfWork uow)
        {
            _tenantContext = tenantContext;
            _uow = uow;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // 1️⃣ Casos que ignoran tenant (Login, etc)
            if (request is ISkipTenantValidation)
                return await next();

            // 2️⃣ Casos que NO requieren tenant explícitamente
            if (request is not IRequireActiveTenant)
                return await next();

            // 3️⃣ Resolver tenant desde middleware/provider
            if (_tenantContext.TenantId is null)
                throw new UnauthorizedAccessException("Tenant no resuelto");

            var repo = _uow.Repository<Tenant, Guid>();

            var tenant = await repo.GetById(_tenantContext.TenantId.Value);

            if (tenant is null)
                throw new UnauthorizedAccessException("Tenant no existe");

            if (!tenant.IsActive)
                throw new UnauthorizedAccessException("Tenant inactivo");

            // 4️⃣ Guardar en contexto para uso posterior
            _tenantContext.SetTenant(tenant);

            return await next();
        }
    }
}

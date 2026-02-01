using Application.Dtos.Auth;
using Application.Interfaces.Persistence;
using Application.Interfaces.Persistence.Filters;
using Application.UseCases.Auth.Mappers;
using Application.UseCases.Auth.Queries;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Auth.Handlers
{
    public class GetUserTenantsHandler
        : IRequestHandler<GetUserTenantsQuery, IReadOnlyList<AuthTenantDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFilterContext _filterContext;

        public GetUserTenantsHandler(IUnitOfWork uow, IFilterContext filterContext)
        {
            _uow = uow;
            _filterContext = filterContext;
        }

        public async Task<IReadOnlyList<AuthTenantDto>> Handle(
            GetUserTenantsQuery request,
            CancellationToken ct)
        {
            _filterContext.DisableTenantFilter = true;

            var repo = _uow.Repository<ApplicationUser, Guid>();

            return await repo.Query()
                .Include(x => x.Tenant)
                .Where(x =>
                    x.Email == request.Email &&
                    x.IsActive &&
                    x.Tenant.IsActive)
                .Select(x => x.Tenant.ToAuthTenantDto())
                .Distinct()
                .ToListAsync(ct);
        }
    }
}

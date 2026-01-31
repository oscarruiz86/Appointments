using Application.Dtos.Auth;
using Application.Interfaces.Persistence;
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

        public GetUserTenantsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<AuthTenantDto>> Handle(
            GetUserTenantsQuery request,
            CancellationToken ct)
        {
            var repo = _uow.Repository<ApplicationUser, Guid>();

            return await repo.Query()
                .IgnoreQueryFilters()
                .Include(x => x.Tenant)
                .Where(x =>
                    x.Email == request.Email &&
                    x.IsActive &&
                    x.Tenant.IsActive)
                .Select(x => x.ToAuthTenantDto())
                .Distinct()
                .ToListAsync(ct);
        }
    }
}

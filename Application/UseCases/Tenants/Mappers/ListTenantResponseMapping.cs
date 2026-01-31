
using Application.Dtos.Tenants;
using Domain.Entities;

namespace Application.UseCases.Tenants.Mappers
{
    public static class ListTenantResponseMapping
    {
        public static List<TenantDto> ToDtoList(this IEnumerable<Tenant> tenants)
        {
            return tenants.Select(t => t.ToDto()).ToList();
        }

    }
}

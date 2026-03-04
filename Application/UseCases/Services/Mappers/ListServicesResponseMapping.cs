
using Application.Dtos.Services;
using Domain.Entities;

namespace Application.UseCases.Services.Mappers
{
    public static class ListServicesResponseMapping
    {
        public static List<ServiceDto> ToDtoList(this IEnumerable<Service> services)
        {
            return services.Select(u => u.ToDto()).ToList();
        }
    }
}

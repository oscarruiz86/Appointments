using Application.Dtos.Services;
using Domain.Entities;

namespace Application.UseCases.Services.Mappers
{
    public static class ServiceMapping
    {
        public static ServiceDto ToDto(this Service request)
        {
            return new ServiceDto
            {
                Name = request.Name,
                Description = request.Description,
                DurationMinutes = request.DurationMinutes,
                Price = request.Price,
                IsActive = request.IsActive
            };
        }
    }
}

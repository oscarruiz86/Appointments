using Application.UseCases.Services.Commands;
using Domain.Entities;

namespace Application.UseCases.Services.Mappers
{
    public static class CreateServiceMapping
    {
        public static Service ToEntity(this CreateServiceCommand request)
        {
            return new Service {
                Name = request.Name,
                Description = request.Description,
                DurationMinutes = request.DurationInMinutes,
                Price = request.Price,
                IsActive = true
            };
        }
    }
}

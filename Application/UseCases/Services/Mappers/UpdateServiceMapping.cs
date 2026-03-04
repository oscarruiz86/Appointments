
using Application.UseCases.Services.Commands;
using Domain.Entities;

namespace Application.UseCases.Services.Mappers
{
    public static class UpdateServiceMapping
    {
        public static void ToEntity(this UpdateServiceCommand request, Service service)
        {
            service.Name = request.Name;
            service.DurationMinutes = request.DurationMinutes;
            service.Price = request.Price;
            service.Description = request.Description;
        }
    }
}

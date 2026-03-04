
using Application.Dtos.Services;
using Domain.Entities;

namespace Application.UseCases.Services.Mappers
{
    public static class CreateServiceResponseMapping
    {
        public static CreateServiceResponseDto ToCreateResponse(this Service user)
        {
            return new CreateServiceResponseDto
            {
                Id = user.Id
            };
        }
    }
}

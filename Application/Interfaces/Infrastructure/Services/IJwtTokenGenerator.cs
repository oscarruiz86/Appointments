using Domain.Entities.Identity;

namespace Application.Interfaces.Infrastructure.Services
{
    public interface IJwtTokenGenerator
    {
        string Generate(ApplicationUser user);
    }
}

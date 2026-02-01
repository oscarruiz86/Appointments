using System.Security.Claims;
using Application.Interfaces.Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        private ClaimsPrincipal? User =>
            _http.HttpContext?.User;

        public Guid UserId
        {
            get
            {
                var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Guid.TryParse(userIdClaim, out var id)
                    ? id
                    : Guid.Empty;
            }
        }

        public IReadOnlyList<string> Roles
        {
            get
            {
                if (User is null)
                    return Array.Empty<string>();

                return User
                    .FindAll(ClaimTypes.Role)
                    .Select(r => r.Value)
                    .ToList();
            }
        }

        public bool IsInRole(string role) =>
            Roles.Contains(role);
    }
}

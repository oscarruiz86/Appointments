using Application.Interfaces.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        private ClaimsPrincipal User => _http.HttpContext!.User;

        public Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public IReadOnlyList<string> Roles =>
            User.FindAll(ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList();

        public bool IsInRole(string role) =>
            Roles.Contains(role);
    }
}

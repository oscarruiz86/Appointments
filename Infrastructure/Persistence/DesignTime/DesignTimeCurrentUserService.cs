
using Application.Interfaces.Infrastructure.Services;

namespace Infrastructure.Persistence.DesignTime
{
    public sealed class DesignTimeCurrentUserService : ICurrentUserService
    {
        public Guid UserId => Guid.Empty;

        public IReadOnlyList<string> Roles =>
            Array.Empty<string>();

        public bool IsInRole(string role) => false;
    }
}

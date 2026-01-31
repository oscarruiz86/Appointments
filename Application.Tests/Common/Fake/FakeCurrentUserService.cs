using Application.Interfaces.Infrastructure.Services;

namespace Application.Tests.Common.Fake
{
    public class FakeCurrentUserService : ICurrentUserService
    {
        public Guid UserId => FakeIds.UserId;

        public IReadOnlyList<string> Roles => new[] { "Admin" };

        public bool IsInRole(string role) => Roles.Contains(role);

        public void SetTenant(Guid tenantId) { }
    }
}

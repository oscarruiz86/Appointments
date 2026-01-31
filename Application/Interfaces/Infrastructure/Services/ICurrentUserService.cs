namespace Application.Interfaces.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        IReadOnlyList<string> Roles { get; }
        bool IsInRole(string role);
    }
}

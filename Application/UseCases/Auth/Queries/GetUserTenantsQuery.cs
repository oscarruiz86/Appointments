
using Application.Dtos.Auth;
using MediatR;

namespace Application.UseCases.Auth.Queries
{
    public class GetUserTenantsQuery
    : IRequest<IReadOnlyList<AuthTenantDto>>
    { 
    public string Email { get; set; } = string.Empty;
    }
}

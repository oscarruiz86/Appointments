
using Application.Common.Tenancy;
using Application.Dtos.Users;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public class ListTenantUsersQuery : IRequest<List<UserDto>>, IRequireActiveTenant
    {
       public bool OnlyActive { get; set; } = true;
    }
}

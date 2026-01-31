using Application.Dtos.Users;
using Application.Interfaces.Infrastructure.Services;
using Application.UseCases.Users.Queries;
using MediatR;

namespace Application.UseCases.Users.Handlers
{
    public class MeHandler : IRequestHandler<MeQuery, UserDto>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IMediator _mediator;

        public MeHandler(
            ICurrentUserService currentUser,
            IMediator mediator)
        {
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public async Task<UserDto> Handle(MeQuery request, CancellationToken ct)
        {
            return await _mediator.Send(
                new GetUserByIdQuery
                {
                    UserId = _currentUser.UserId
                },
                ct
            );
        }
    }
}

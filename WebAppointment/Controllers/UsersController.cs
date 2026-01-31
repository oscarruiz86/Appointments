using Application.Dtos.Users;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(
        IMediator mediator
        )
    {

        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }


    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,TenantAdmin")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
    {
        await _mediator.Send(new UpdateUserCommand
        {
            UserId = id,
            FullName = dto.FullName,
            Phone = dto.Phone
        });

        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,TenantAdmin")]
    public async Task<IActionResult> Disable(Guid id)
    {
        await _mediator.Send(new DisableUserCommand { UserId= id });

        return NoContent();
    }


    [HttpPatch("{id:guid}/password")]
    public async Task<IActionResult> ChangePassword(Guid id, ChangePasswordDto dto)
    {
        await _mediator.Send(new ChangePasswordCommand { UserId= id, NewPassword=  dto.NewPassword });

        return NoContent();
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { UserId= id });

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,TenantAdmin")]
    public async Task<IActionResult> GetTenantUsers([FromQuery] bool onlyActive = false)
    {
        var users = await _mediator.Send(
            new ListTenantUsersQuery { OnlyActive = onlyActive }
        );

        return Ok(users);
    }


    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var result = await _mediator.Send(new MeQuery());
        return Ok(result);
    }
}

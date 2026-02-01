
using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("impersonate")]
        public async Task<IActionResult> Login(ImpersonateCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("tenants")]
        public async Task<IActionResult> GetTenants(string email) {
            var result = await _mediator.Send(new GetUserTenantsQuery { Email = email });
            return Ok(result);
        }
    }

}

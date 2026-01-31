using Application.UseCases.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpersonateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImpersonateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("impersonate")]
        public async Task<IActionResult> Login(ImpersonateCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }

}

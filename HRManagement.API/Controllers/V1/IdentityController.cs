using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Features.Identities.Commands;
using Microsoft.AspNetCore.Mvc;


namespace HRManagement.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : ApiControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var command = new RegisterCommand { RegisterDto = registerDto };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand { LoginDto = loginDto };
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}

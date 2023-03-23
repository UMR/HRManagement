using HRManagement.Application.Contracts.Services;
using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Dtos.RefreshTokens;
using Microsoft.AspNetCore.Mvc;


namespace HRManagement.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : ApiControllerBase
    {
        private readonly IdentityService _identityService;

        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {            
            return Ok(await _identityService.RegisterAsync(registerDto));
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Ok(await _identityService.LoginAsync(loginDto));
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            return Ok(await _identityService.RefreshTokenAsync(refreshTokenDto));
        }
    }
}

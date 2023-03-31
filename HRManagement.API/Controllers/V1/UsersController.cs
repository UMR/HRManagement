using HRManagement.Application.Contracts.Services;
using HRManagement.Application.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public UsersController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<List<UserForListDto>>> GetUsers()
        {
            return await _userService.GetUsersAsync();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserForListDto>> GetUser(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }        

        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserForUpdaterDto updateUserDto)
        {
            var response = await _userService.UpdateUser(updateUserDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            return Ok(response);
        }
    }
}

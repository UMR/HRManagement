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

        public UsersController(IUserService userService)
        {
            _userService = userService;    
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserForListDto>>> GetUsers()
        {
            return await _userService.GetUsersAsync();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserForListDto>> GetUser(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }        

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserForUpdaterDto updateUserDto)
        {
            var response = await _userService.UpdateUser(updateUserDto);
            return Ok(response);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            return Ok(response);
        }
    }
}

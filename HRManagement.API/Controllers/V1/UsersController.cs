using HRManagement.Application.Contracts.Services;
using HRManagement.Application.Dtos.Users;
using HRManagement.Application.Features.Users.Commands;
using HRManagement.Application.Features.Users.Queries;
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

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult> AddUser([FromBody] UserForCreateDto createUserDto)
        {
            var command = new CreateUserCommand { CreateUserDto = createUserDto };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserForUpdaterDto updateUserDto)
        {
            var command = new UpdateUserCommand { UpdateUserDto = updateUserDto };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { UserId = id };
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}

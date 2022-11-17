using HRManagement.Application.Dtos.Users;
using HRManagement.Application.Features.Users.Commands;
using HRManagement.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return await Mediator.Send(new GetUsersQuery());
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            return await Mediator.Send(new GetUserByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] CreateUserDto createUserDto)
        {
            var command = new CreateUserCommand { CreateUserDto = createUserDto };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
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

using HRManagement.Application.Contracts.Services;
using HRManagement.Application.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(policy: "Permission.Role.View")]
        [HttpGet("GetRoles")]
        public async Task<ActionResult<List<RoleForListDto>>> GetRoles()
        {
            return await _roleService.GetRolesAsync();
        }

        [Authorize(policy: "Permission.Role.View")]
        [HttpGet("GetRole/{id}")]
        public async Task<ActionResult<RoleForListDto>> GetRole(int id)
        {
            return await _roleService.GetRoleByIdAsync(id);
        }

        [Authorize(policy: "Permission.Role.Create")]
        [HttpPost("CreateRole")]
        public async Task<ActionResult> CreateRole([FromBody] RoleForCreateDto roleForCreateDto)
        {
            var response = await _roleService.CreateRole(roleForCreateDto);
            return Ok(response);
        }

        [Authorize(policy: "Permission.Role.Update")]
        [HttpPut("UpdateRole")]
        public async Task<ActionResult> UpdateRole([FromBody] RoleForUpdateDto roleForUpdateDto)
        {
            var response = await _roleService.UpdateRole(roleForUpdateDto);
            return Ok(response);
        }

        [Authorize(policy: "Permission.Role.Delete")]
        [HttpDelete("DeleteRole/{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteRole(id);
            return Ok(response);
        }
        
        [Authorize(policy: "Permission.Role.Create")]        
        [HttpPost("AssignRolesToUser")]
        public async Task<ActionResult> AssignRolesToUser(AssignRolesToUserDto assignRolesToUserDto)
        {
            var response = await _roleService.AssignRolesToUserAsync(assignRolesToUserDto);
            return Ok(response);
        }
    }
}

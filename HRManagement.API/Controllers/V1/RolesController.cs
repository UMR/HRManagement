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

        [Authorize(policy: "role:read")]
        [HttpGet(Name = "GetRoles")]
        public async Task<ActionResult<List<RoleForListDto>>> GetRoles()
        {
            return await _roleService.GetRolesAsync();
        }

        [Authorize(policy: "role:read")]
        [HttpGet("{id}", Name = "GetRole")]
        public async Task<ActionResult<RoleForListDto>> GetRole(int id)
        {
            return await _roleService.GetRoleByIdAsync(id);
        }

        [Authorize(policy: "role:create")]
        [HttpPost(Name = "CreateRole")]
        public async Task<ActionResult> CreateRole([FromBody] RoleForCreateDto roleForCreateDto)
        {
            var response = await _roleService.CreateRole(roleForCreateDto);
            return Ok(response);
        }

        [Authorize(policy: "role:update")]
        [HttpPut(Name = "UpdateRole")]
        public async Task<ActionResult> UpdateRole([FromBody] RoleForUpdateDto roleForUpdateDto)
        {
            var response = await _roleService.UpdateRole(roleForUpdateDto);
            return Ok(response);
        }

        [Authorize(policy: "role:delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteRole(id);
            return Ok(response);
        }
    }
}

using HRManagement.Application.Contracts.Services;
using HRManagement.Application.Dtos.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet(Name = "GetRoles")]
        public async Task<ActionResult<List<RoleForListDto>>> GetRoles()
        {
            return await _permissionService.GetRolesAsync();
        }

        [HttpGet("{id}", Name = "GetRole")]
        public async Task<ActionResult<RoleForListDto>> GetRole(int id)
        {
            return await _permissionService.GetRoleByIdAsync(id);
        }

        [HttpPost(Name = "CreateRole")]
        public async Task<ActionResult> CreateRole([FromBody] RoleForCreateDto roleForCreateDto)
        {
            var response = await _permissionService.CreateRole(roleForCreateDto);
            return Ok(response);
        }

        [HttpPut(Name = "UpdateRole")]
        public async Task<ActionResult> UpdateRole([FromBody] RoleForUpdateDto roleForUpdateDto)
        {
            var response = await _permissionService.UpdateRole(roleForUpdateDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var response = await _permissionService.DeleteRole(id);
            return Ok(response);
        }
    }
}

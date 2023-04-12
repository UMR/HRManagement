using HRManagement.Application.Contracts.Services;
using HRManagement.Application.Dtos.Permissions;
using HRManagement.Application.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(policy: "Permission.AssignPermissionsToRole")]
        [HttpGet("GetPermissions")]
        public  async Task<ActionResult<List<PermissionForListDto>>> GetPermissions()
        {
            return  await _permissionService.GetPermissionsAsync();
        }

        [Authorize(policy: "Permission.AssignPermissionsToRole")]
        [HttpGet("GetPermission/{id}")]
        public async Task<ActionResult<PermissionForListDto>> GetPermission(int id)
        {
            return await _permissionService.GetPermissionByIdAsync(id);
        }

        [Authorize(policy: "Permission.AssignPermissionsToRole")]
        [HttpPost("CreatePermission")]
        public async Task<ActionResult> CreatePermission([FromBody] PermissionForCreateDto permissionForCreateDto)
        {
            var response = await _permissionService.CreatePermission(permissionForCreateDto);
            return Ok(response);
        }

        [Authorize(policy: "Permission.AssignPermissionsToRole")]
        [HttpPut("UpdatePermission")]
        public async Task<ActionResult> UpdatePermission([FromBody] PermissionForUpdateDto permissionForUpdateDto)
        {
            var response = await _permissionService.UpdatePermission(permissionForUpdateDto);
            return Ok(response);
        }

        [Authorize(policy: "Permission.AssignPermissionsToRole")]
        [HttpDelete("DeletePermission/{id}")]
        public async Task<ActionResult> DeletePermission(int id)
        {
            var response = await _permissionService.DeletePermission(id);
            return Ok(response);
        }

        [Authorize(policy: "Permission.AssignPermissionsToRole")]
        [HttpPost("AssignPermissionsToRole")]
        public async Task<ActionResult> AssignPermissionsToRole(AssignPermissionsToRoleDto assignPermissionsToRoleDto)
        {
            var response = await _permissionService.AssignPermissionsToRoleAsync(assignPermissionsToRoleDto);
            return Ok(response);
        }
    }
}

using HRManagement.Application.Contracts.Services;
using HRManagement.Application.Dtos.Permissions;
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

        [HttpGet(Name = "GetPermissions")]
        public  async Task<ActionResult<List<PermissionForListDto>>> GetPermissions()
        {
            return  await _permissionService.GetPermissionsAsync();
        }

        [HttpGet("{id}", Name = "GetPermission")]
        public async Task<ActionResult<PermissionForListDto>> GetPermission(int id)
        {
            return await _permissionService.GetPermissionByIdAsync(id);
        }

        [HttpPost(Name = "CreatePermission")]
        public async Task<ActionResult> CreatePermission([FromBody] PermissionForCreateDto permissionForCreateDto)
        {
            var response = await _permissionService.CreatePermission(permissionForCreateDto);
            return Ok(response);
        }

        [HttpPut(Name = "UpdatePermission")]
        public async Task<ActionResult> UpdatePermission([FromBody] PermissionForUpdateDto permissionForUpdateDto)
        {
            var response = await _permissionService.UpdatePermission(permissionForUpdateDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePermission(int id)
        {
            var response = await _permissionService.DeletePermission(id);
            return Ok(response);
        }
    }
}

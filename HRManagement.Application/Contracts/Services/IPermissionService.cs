using HRManagement.Application.Dtos.Permissions;
using HRManagement.Application.Wrapper;

namespace HRManagement.Application.Contracts.Services
{
    public interface IPermissionService
    {
        Task<BaseCommandResponse> CreatePermission(PermissionForCreateDto permissionForCreateDto);
        Task<BaseCommandResponse> DeletePermission(int id);
        Task<PermissionForListDto> GetPermissionByIdAsync(int id);
        Task<List<PermissionForListDto>> GetPermissionsAsync();
        Task<BaseCommandResponse> UpdatePermission(PermissionForUpdateDto permissionForUpdateDto);
    }
}
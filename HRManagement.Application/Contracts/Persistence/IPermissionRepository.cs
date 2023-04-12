using HRManagement.Domain.Entities;

namespace HRManagement.Application.Contracts.Persistence
{
    public interface IPermissionRepository
    {
        Task<Permission> CreatePermissionAsync(Permission permission);
        Task<bool> DeletePermissionAsync(Permission permission);
        Task<Permission> GetPermissionByIdAsync(int id);
        Task<IEnumerable<Permission>> GetPermissionsAsync();
        Task<bool> UpdatePermissionAsync(Permission permission);
        Task<bool> AssignPermissionsToRoleAsync(int roleId, int[] permissionIds);
    }
}
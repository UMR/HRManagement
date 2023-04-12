using HRManagement.Domain.Entities;

namespace HRManagement.Application.Contracts.Persistence
{
    public interface IRoleRepository
    {
        Task<Role> CreateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(Role role);
        Task<Role> GetIdByRoleNameAsync(string rolename);
        Task<Role> GetRoleByIdAsync(int id);
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> AssignRolesToUserAsync(int userId, int[] roleIds);
    }
}
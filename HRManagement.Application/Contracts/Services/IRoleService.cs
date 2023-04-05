using HRManagement.Application.Dtos.Roles;
using HRManagement.Application.Wrapper;

namespace HRManagement.Application.Contracts.Services
{
    public interface IRoleService
    {
        Task<BaseCommandResponse> CreateRole(RoleForCreateDto roleForCreateDto);
        Task<BaseCommandResponse> DeleteRole(int id);
        Task<RoleForListDto> GetRoleByIdAsync(int id);
        Task<List<RoleForListDto>> GetRolesAsync();
        Task<BaseCommandResponse> UpdateRole(RoleForUpdateDto roleForUpdateDto);
    }
}
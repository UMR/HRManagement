using HRManagement.Application.Dtos.Users;
using HRManagement.Application.Wrapper;

namespace HRManagement.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<UserForListDto> GetUserByIdAsync(int id);

        Task<List<UserForListDto>> GetUsersAsync();

        Task<BaseCommandResponse> DeleteUser(int userId);
        
    }
}
using HRManagement.Application.Dtos.Identities;

namespace HRManagement.Application.Contracts
{
    public interface IIdentityService
    {
        Task<AuthResult> RegisterAsync(string email, string password);
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RefreshTokenAsync(string token, string refreshToken);
    }
}

using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Dtos.RefreshTokens;

namespace HRManagement.Application.Contracts.Services
{
    public interface IIdentityService
    {
        Task<AuthResult> RegisterAsync(RegisterDto registerDto);
        Task<AuthResult> LoginAsync(LoginDto loginDto);
        Task<AuthResult> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    }
}

using HRManagement.Domain.Entities;

namespace HRManagement.Application.Contracts.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync();

        Task<RefreshToken> GetRefreshTokenByIdAsync(int id);

        Task<bool> CreateRefreshTokenAsync(RefreshToken refreshToken);

        Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken);

        Task<bool> DeleteRefreshTokenAsync(RefreshToken refreshToken);
    }
}

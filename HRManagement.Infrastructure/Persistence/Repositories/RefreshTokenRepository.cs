using HRManagement.Application.Contracts.Persistence;
using HRManagement.Domain.Entities;
using HRManagement.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly HRDbContext _dbContext;

        public RefreshTokenRepository(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync()
        {
            return await _dbContext.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenByIdAsync(int id)
        {
            return await _dbContext.RefreshTokens.FindAsync(id);
        }

        public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string refreshToken) 
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(r=>r.Token == refreshToken);
        }

        public async Task<bool> CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _dbContext.Entry(refreshToken).State = EntityState.Modified;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> DeleteRefreshTokenAsync(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Remove(refreshToken);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }
    }
}

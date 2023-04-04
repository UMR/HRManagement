using HRManagement.Application.Contracts.Persistence;
using HRManagement.Domain.Entities;
using HRManagement.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HRDbContext _dbContext;

        public UserRepository(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }        

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email) 
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u=>u.Email == email.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> CheckPasswordAsync(User user, string passwordHash, string passwordSalt)
        {
            var userFromRepo = await GetUserByEmailAsync(user.Email);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;            
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _dbContext.Users.Remove(user);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }
    }
}

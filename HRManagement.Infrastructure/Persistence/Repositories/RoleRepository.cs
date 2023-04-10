using HRManagement.Application.Contracts.Persistence;
using HRManagement.Domain.Entities;
using HRManagement.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HRDbContext _dbContext;

        public RoleRepository(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }

        public async Task<Role> GetIdByRoleNameAsync(string name)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(u => u.Name == name.ToLower());
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _dbContext.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            _dbContext.Entry(role).State = EntityState.Modified;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> DeleteRoleAsync(Role role)
        {
            _dbContext.Roles.Remove(role);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> AssignRolesToUserAsync(int userId, int roleId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            var userRoles = new List<UserRole>()
            {
                new UserRole() { Id = userId, RoleId = roleId },
                new UserRole() { Id = userId, RoleId = roleId },
            };

            await _dbContext.UserRoles.AddRangeAsync(userRoles);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

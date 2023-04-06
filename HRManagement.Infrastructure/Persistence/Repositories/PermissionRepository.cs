using HRManagement.Application.Contracts.Persistence;
using HRManagement.Domain.Entities;
using HRManagement.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Infrastructure.Persistence.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly HRDbContext _dbContext;

        public PermissionRepository(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await _dbContext.Permissions.AsNoTracking().ToListAsync();
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _dbContext.Permissions.FindAsync(id);
        }

        public async Task<Permission> CreatePermissionAsync(Permission permission)
        {
            await _dbContext.Permissions.AddAsync(permission);
            await _dbContext.SaveChangesAsync();
            return permission;
        }

        public async Task<bool> UpdatePermissionAsync(Permission permission)
        {
            _dbContext.Entry(permission).State = EntityState.Modified;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> DeletePermissionAsync(Permission permission)
        {
            _dbContext.Permissions.Remove(permission);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }
    }
}

using HRManagement.Application.Contracts.Infrastructure;
using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace HRManagement.Infrastructure.Persistence.Data
{
    public class HRDbContext: DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public HRDbContext(DbContextOptions<HRDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseAuditableEntity)entityEntry.Entity).LastModified = DateTime.Now;
                ((BaseAuditableEntity)entityEntry.Entity).LastModified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseAuditableEntity)entityEntry.Entity).Created = DateTime.Now;
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedBy = _currentUserService.UserId;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseAuditableEntity)entityEntry.Entity).LastModifiedBy = _currentUserService.UserId;
                ((BaseAuditableEntity)entityEntry.Entity).LastModified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseAuditableEntity)entityEntry.Entity).Created = DateTime.Now;
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedBy = _currentUserService.UserId;
                }
            }            

            return base.SaveChangesAsync(cancellationToken);
        }        
    }
}

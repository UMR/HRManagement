using HRManagement.Application.Contracts;
using HRManagement.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HRManagement.Infrastructure.Persistence.Data
{
    public class SeedData
    {
        public static void PopulateDb(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            AddInitialData(serviceScope.ServiceProvider.GetService<HRDbContext>(), serviceScope.ServiceProvider.GetService<IPasswordHasher>());
        }

        private static void AddInitialData(HRDbContext context, IPasswordHasher passwordHasher)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                byte[] passwordHash, passwordSalt;

                passwordHasher.CreatePasswordHash("123456", out passwordHash, out passwordSalt);

                var orders = new List<User>()
                {
                    new User
                    {
                        FirstName = "Captain",
                        LastName = "Black",
                        Email = "test@test.com",
                        PasswordHash=passwordHash,
                        PasswordSalt=passwordSalt
                    }
                };

                context.Users.AddRange(orders);
                context.SaveChanges();
            }

            if (!context.Permissions.Any())
            {
                var permissions = new List<Permission>()
                {
                    new Permission { Name = "role:read"},
                    new Permission { Name = "role:create"},
                    new Permission { Name = "role:update"},
                    new Permission { Name = "role:delete"}                    
                };

                context.Permissions.AddRange(permissions);
                context.SaveChanges();
            }
        }
    }
}

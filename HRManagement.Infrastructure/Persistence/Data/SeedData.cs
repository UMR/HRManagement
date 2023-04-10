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
                        Id = 1,
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
                    new Permission { Id=1, Name = "Permission.Role.Read"},
                    new Permission { Id=2, Name = "Permission.Role.Read"},
                    new Permission { Id=3, Name = "Permission.Role.Read"},
                    new Permission { Id=4, Name = "Permission.Role.Read"}
                };

                context.Permissions.AddRange(permissions);
                context.SaveChanges();
            }
        }
    }
}

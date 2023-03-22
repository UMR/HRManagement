using HRManagement.Application.Contracts;
using HRManagement.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HRManagement.Infrastructure.Persistence.Data
{
    public static class SeedData
    {
        //private readonly IPasswordHasher _passwordHasher;
        //public static void PopulateDb(IApplicationBuilder app, IPasswordHasher passwordHasher)
        //{
        //    _passwordHasher = passwordHasher;
        //    using var serviceScope = app.ApplicationServices.CreateScope();
        //    AddInitialData(serviceScope.ServiceProvider.GetService<HRDbContext>()!);
        //}

        //private static void AddInitialData(HRDbContext context)
        //{
        //    context.Database.EnsureCreated();

        //    if (!context.Users.Any())
        //    {
        //        var orders = new List<User>()
        //        {
        //            new User
        //            {
        //                FirstName = "Captain",
        //                LastName = "Black",
        //                Email = "test@test.com",
        //                PasswordHash
        //            }                    
        //        };

        //        context.Users.AddRange(orders);
        //        context.SaveChanges();
        //    }
        //}
    }
}

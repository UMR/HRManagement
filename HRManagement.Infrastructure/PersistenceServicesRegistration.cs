using HRManagement.Application.Contracts.Infrastructure;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Infrastructure.Persistence.Data;
using HRManagement.Infrastructure.Persistence.Repositories;
using HRManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRManagement.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HRDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly(typeof(HRDbContext).Assembly.FullName)
                ));

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

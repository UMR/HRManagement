using HRManagement.Application.Contracts.Persistence;
using HRManagement.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HRManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

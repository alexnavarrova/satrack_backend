using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Satrack.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Satrack.Application.Contracts.Persistence;
using Satrack.Infraestructure.Persistence;

namespace Satrack.Infraestructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<ITaskRepository, TaskRepository>();


            return services;
        }
    }
}


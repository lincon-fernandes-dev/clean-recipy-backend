using Application.Interfaces.Services;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infra.Data;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
                ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));



            services.AddAutoMapper(cfg => cfg.AddProfile<DomainToDTOMappingProfile>());

            return services;
        }
    }
}

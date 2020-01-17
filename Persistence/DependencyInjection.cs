using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Patronage_NET.Application.Common.Interfaces;

namespace Patronage_NET.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PatronageDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("PatronageDbContext")));

            services.AddScoped<IPatronageDbContext>(provider =>
                     provider.GetService<PatronageDbContext>());

            return services;
        }
    }
}

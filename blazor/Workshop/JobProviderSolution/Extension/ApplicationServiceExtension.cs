//using JobProviderApp.Helpers;
//using JobProviderApp.Interface;
using JobProviderSolution.Model;
//using JobProviderApp.Repository;
//using JobProviderApp.Service;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

namespace JobProviderSolution.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices
           (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<JobProviderAppDbContext>(options =>
              options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}


using Domain_js.Models;
using Domain_js.Service;
using Domain_js.Service.Authuser;
using Domain_js.Service.Authuser.Interfaces;
using Domain_js.Service.Login;
using Domain_js.Service.Login.Interfaces;
using Domain_js.Service.SignUp;
using Domain_js.Service.SignUp.Interfaces;
using Domain_js.Service.User;
using Domain_js.Service.User.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain_js.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<HireMeNowDbContext>(options =>
               options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<ISignUpRequestService, SignUpRequestService>();
            services.AddScoped<ISignUpRequestRepository, SignUpRequestRepository>();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoginRequestService, LoginRequestService>();
            services.AddScoped<ILoginRequestRepository, LoginRequestRepository>();

            return services;
        }
    }
}

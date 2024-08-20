
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Identity.Models;
using MuseumManagementSystem.Identity.Services;

namespace MuseumManagementSystem.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection AddIdentityServicesAndConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
    
            services.AddDbContext<ApplicationIdentityDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationIdentityDbContext).Assembly.FullName)
            ));

            services.Configure<SecurityStampValidatorOptions>( option =>
                option.ValidationInterval = TimeSpan.FromMinutes(1));


            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;

            })
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}


using MuseumManagementSystem.Application.Models.Infrastructure;

using MuseumManagementSystem.Infrastructure.Mail;
using MuseumManagementSystem.Application.Contracts.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MuseumManagementSystem.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            //services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}

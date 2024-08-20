using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MuseumManagementSystem.Application.Contracts.Persistence.Base;
using MuseumManagementSystem.Persistence.Repositories.Base;

namespace MuseumManagementSystem.Persistence
{
    public static partial class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServicesAndConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
        
        services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
             b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
              ));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITimePeriodsRepository, TimePeriodsRepository>();
            services.AddScoped<IArtifactImagesRepository, ArtifactImagesRepository>();
            services.AddScoped<IArtifactTypesRepository, ArtifactTypesRepository>();
            services.AddScoped<IMaterialsRepository, MaterialsRepository>();
            services.AddScoped<IBioDegsRepository, BioDegsRepository>();
            services.AddScoped<ISafesRepository, SafesRepository>();
            services.AddScoped<IStowagesRepository, StowagesRepository>();
            services.AddScoped<IArtifactConditionsRepository, ArtifactConditionsRepository>();
            services.AddScoped<IArtifactsRepository, ArtifactsRepository>();
            return services;
        }
    }
}

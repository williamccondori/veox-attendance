using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.User.Application.Contexts.Implementations;
using Veox.Attendance.User.Application.Contexts.Interfaces;
using Veox.Attendance.User.Application.Services.Implementations;
using Veox.Attendance.User.Application.Services.Interfaces;
using Veox.Attendance.User.Domain.Repositories;
using Veox.Attendance.User.Infraestructure.MongoDb.Contexts.Implementations;
using Veox.Attendance.User.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.User.Infraestructure.MongoDb.Repositories;

namespace Veox.Attendance.User.IoC
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            // Contexts.
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddScoped<ApplicationContext>();
            services.AddScoped<IApplicationContext>(x => x.GetRequiredService<ApplicationContext>());

            // Repositories.
            services.AddScoped<IUserRepository, UserRepository>();

            // Application services.
            services.AddScoped<IUserService, UserService>();
        }
    }
}
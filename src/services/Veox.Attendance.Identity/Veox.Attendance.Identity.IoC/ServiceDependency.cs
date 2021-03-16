using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Identity.Application.Services.Implementations;
using Veox.Attendance.Identity.Application.Services.Interfaces;
using Veox.Attendance.Identity.Domain.Repositories;
using Veox.Attendance.Identity.Infraestructure.Jwt.Builders.Implementations;
using Veox.Attendance.Identity.Infraestructure.Jwt.Builders.Interfaces;
using Veox.Attendance.Identity.Infraestructure.MongoDb.Contexts.Implementations;
using Veox.Attendance.Identity.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.Identity.Infraestructure.MongoDb.Repositories;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Implementations;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces;

namespace Veox.Attendance.Identity.IoC
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            
            services.AddScoped<IJwtBuilder, JwtBuilder>();

            // Rabbit MQ producers.
            services.AddScoped<IEmailProducer, EmailProducer>();
            services.AddScoped<IUserProducer, UserProducer>();

            // Repositories.
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICodeRepository, CodeRepository>();

            // Applications services.
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
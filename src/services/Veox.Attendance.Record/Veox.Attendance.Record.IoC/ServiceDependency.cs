using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Record.Application.Contexts.Implementations;
using Veox.Attendance.Record.Application.Contexts.Interfaces;
using Veox.Attendance.Record.Application.Services.Implementations;
using Veox.Attendance.Record.Application.Services.Interfaces;
using Veox.Attendance.Record.Domain.Repositories;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts.Implementations;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.Record.Infraestructure.MongoDb.Repositories;

namespace Veox.Attendance.Record.IoC
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
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Application services.
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
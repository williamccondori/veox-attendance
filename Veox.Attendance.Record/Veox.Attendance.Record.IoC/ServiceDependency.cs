using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<IMongoDbContext, MongoDbContext>();

            services.AddTransient<IRecordRepository, RecordRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IRecordService, RecordService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
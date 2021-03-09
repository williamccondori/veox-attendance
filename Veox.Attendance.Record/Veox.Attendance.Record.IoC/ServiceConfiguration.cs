using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Record.Application.Interfaces.Services;
using Veox.Attendance.Record.Application.Services;
using Veox.Attendance.Record.Domain.Repositories;
using Veox.Attendance.Record.Infraestructure.MongoDb.Contexts;
using Veox.Attendance.Record.Infraestructure.MongoDb.Repositories;

namespace Veox.Attendance.Record.IoC
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(s => new
                MongoDbContext("localhost", "7000", "root", "ficticio", "attendance-record"));
            
            services.AddTransient<IRecordRepository, RecordRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            
            services.AddTransient<IRecordService, RecordService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
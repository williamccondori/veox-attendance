using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Record.Application.Services.Implementations;
using Veox.Attendance.Record.Application.Services.Interfaces;
using Veox.Attendance.Record.Domain.Repositories;
using Veox.Attendance.Record.Infraestructure.MongoDb.Repositories;

namespace Veox.Attendance.Record.IoC
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRecordRepository, RecordRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IRecordService, RecordService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
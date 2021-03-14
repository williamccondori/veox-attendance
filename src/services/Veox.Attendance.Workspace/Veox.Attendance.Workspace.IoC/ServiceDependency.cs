using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Workspace.Application.Services.Implementations;
using Veox.Attendance.Workspace.Application.Services.Interfaces;
using Veox.Attendance.Workspace.Domain.Repositories;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Implementations;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Contexts.Interfaces;
using Veox.Attendance.Workspace.Infraestructure.MongoDb.Repositories;

namespace Veox.Attendance.Workspace.IoC
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();

            services.AddScoped<IWorkspaceService, WorkspaceService>();
        }
    }
}
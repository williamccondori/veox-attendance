using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Workspace.Application.Services.Implementations;
using Veox.Attendance.Workspace.Application.Services.Interfaces;

namespace Veox.Attendance.Workspace.IoC
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IWorkspaceService, WorkspaceService>();
        }
    }
}
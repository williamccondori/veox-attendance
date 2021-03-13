using Microsoft.Extensions.DependencyInjection;
using Veox.Attendance.Workspace.Application.Services;
using Veox.Attendance.Workspace.Application.Services.Implementations;

namespace Veox.Attendance.Workspace.IoC
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWorkspaceService, WorkspaceService>();
        }
    }
}
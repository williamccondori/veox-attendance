using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Services.Interfaces.Common;

namespace Veox.Attendance.Workspace.Application.Services.Interfaces
{
    public interface IWorkspaceService : IBaseService
    {
        Task<List<WorkspaceResponse>> GetAsync();
        Task<WorkspaceResponse> CreateAsync(WorkspaceRequest workspaceRequest);
        Task<WorkspaceResponse> UpdateAsync(string workspaceId, WorkspaceRequest workspaceRequest);
        Task<EmployeeResponse> AddEmployeeAsync(string workspaceId, EmployeeRequest employeeRequest);
    }
}
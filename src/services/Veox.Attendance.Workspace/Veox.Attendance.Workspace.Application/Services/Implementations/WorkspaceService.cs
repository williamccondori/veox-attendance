using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Services.Implementations.Common;
using Veox.Attendance.Workspace.Application.Services.Interfaces;

namespace Veox.Attendance.Workspace.Application.Services.Implementations
{
    public class WorkspaceService : BaseService, IWorkspaceService
    {
        public Task<List<WorkspaceResponse>> GetAsync()
        {
            return Task.FromResult(new List<WorkspaceResponse>()
            {
                new WorkspaceResponse()
                {
                    Id = "",
                    ImagePath = "https://dummyimage.com/300x300/000/fff/&text=+VE+",
                }
            });
        }

        public Task<WorkspaceResponse> CreateAsync(WorkspaceRequest workspaceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<WorkspaceResponse> UpdateAsync(string workspaceId, WorkspaceRequest workspaceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> AddEmployeeAsync(string workspaceId, EmployeeRequest employeeRequest)
        {
            throw new NotImplementedException();
        }
    }
}
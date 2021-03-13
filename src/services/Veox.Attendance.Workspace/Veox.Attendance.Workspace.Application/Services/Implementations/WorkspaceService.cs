using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Wrappers;

namespace Veox.Attendance.Workspace.Application.Services.Implementations
{
    public class WorkspaceService : IWorkspaceService
    {
        public async Task<Response<IEnumerable<WorkspaceResponse>>> GetByUserAsync()
        {
            var workspaces = new List<WorkspaceResponse>
            {
                new WorkspaceResponse {Id = Guid.NewGuid(), Name = "Veox", Description = "Veox", Image = "image.png"},
                new WorkspaceResponse {Id = Guid.NewGuid(), Name = "Neax", Description = "Neax", Image = "image.png"}
            };

            var response = new Response<IEnumerable<WorkspaceResponse>>(workspaces);
            
            return await Task.FromResult(response);
        }

        public Task<Response<WorkspaceResponse>> CreateAsync(WorkspaceModel workspace)
        {
            throw new NotImplementedException();
        }

        public Task<Response<WorkspaceResponse>> UpdateAsync(WorkspaceModel workspace)
        {
            throw new NotImplementedException();
        }
    }
}
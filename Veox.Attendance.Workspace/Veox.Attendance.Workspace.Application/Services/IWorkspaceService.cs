using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Wrappers;

namespace Veox.Attendance.Workspace.Application.Services
{
    public interface IWorkspaceService
    {

        Task<Response<IEnumerable<WorkspaceResponse>>> GetByUserAsync();

        /// <summary>
        /// Create a new workspace.
        /// </summary>
        /// <param name="workspace">Workspace model.</param>
        /// <returns>Workspace model created.</returns>
        Task<Response<WorkspaceResponse>> CreateAsync(WorkspaceModel workspace);

        /// <summary>
        /// Update a existing workspace.
        /// </summary>
        /// <param name="workspace">Workspace model.</param>
        /// <returns>Workspace model updated.</returns>
        Task<Response<WorkspaceResponse>> UpdateAsync(WorkspaceModel workspace);
    }
}
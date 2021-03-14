using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Domain.Entities;

namespace Veox.Attendance.Workspace.Domain.Repositories
{
    public interface IWorkspaceRepository
    {
        Task<WorkspaceEntity> GetByIdentifier(string workspaceIdentifier);
        Task<IEnumerable<WorkspaceEntity>> GetByEmployee(string employeeId);
        Task<WorkspaceEntity> Create(WorkspaceEntity workspaceEntity);
        Task<WorkspaceEntity> Update(string workspaceId, WorkspaceEntity workspaceEntity);
    }
}
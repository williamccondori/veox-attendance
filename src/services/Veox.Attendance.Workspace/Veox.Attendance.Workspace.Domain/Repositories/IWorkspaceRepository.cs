using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Domain.Entities;

namespace Veox.Attendance.Workspace.Domain.Repositories
{
    public interface IWorkspaceRepository
    {
        Task<WorkspaceEntity> GetById(string workspaceId);
        Task<WorkspaceEntity> GetByIdentifier(string workspaceIdentifier);
        Task<IEnumerable<WorkspaceEntity>> GetAllByEmployee(string employeeId);
        Task<WorkspaceEntity> Create(WorkspaceEntity workspaceEntity);
        Task<WorkspaceEntity> Update(string workspaceId, WorkspaceEntity workspaceEntity);
    }
}
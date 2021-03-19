using System.Threading.Tasks;
using Veox.Attendance.Workspace.Domain.Entities;

namespace Veox.Attendance.Workspace.Domain.Repositories
{
    public interface IGroupRepository
    {
        Task<GroupEntity> Create(GroupEntity groupEntity);
    }
}
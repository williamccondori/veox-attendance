using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Domain.Entities;

namespace Veox.Attendance.Workspace.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetAllByUserId(string userId);
        Task<EmployeeEntity> Create(EmployeeEntity employeeEntity);
    }
}
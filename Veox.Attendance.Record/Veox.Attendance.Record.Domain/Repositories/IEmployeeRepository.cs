using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Record.Domain.Entities;

namespace Veox.Attendance.Record.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetAllByWorksapce(string workspaceId);
        
        Task<EmployeeEntity> GetById(string employeeId);

        Task<EmployeeEntity> GetByDocumentNumberAndWorkspace(string documentNumber, string workspaceId);

        Task<EmployeeEntity> Create(EmployeeEntity employeeEntity);
        
        Task<EmployeeEntity> Update(string employeeId, EmployeeEntity employeeEntity);
    }
}
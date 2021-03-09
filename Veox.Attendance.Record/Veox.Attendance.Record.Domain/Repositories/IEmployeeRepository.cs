using System;
using System.Threading.Tasks;
using Veox.Attendance.Record.Domain.Entities;

namespace Veox.Attendance.Record.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity> GetById(string employeeId);

        Task<EmployeeEntity> GetByDocumentNumber(string documentNumber);

        Task<EmployeeEntity> Create(EmployeeEntity employeeEntity);
        
        Task<EmployeeEntity> Update(string employeeId, EmployeeEntity employeeEntity);
    }
}
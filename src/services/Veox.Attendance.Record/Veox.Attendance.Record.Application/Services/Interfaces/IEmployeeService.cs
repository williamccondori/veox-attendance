using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Services.Interfaces.Common;

namespace Veox.Attendance.Record.Application.Services.Interfaces
{
    public interface IEmployeeService : IBaseService
    {
        /// <summary>
        /// Create or update a employee.
        /// </summary>
        /// <param name="employeeRequest">Employee model.</param>
        /// <returns>Task completed.</returns>
        Task SaveAsync(EmployeeRequest employeeRequest);
        
        
        /// <summary>
        /// Delete a employee.
        /// </summary>
        /// <param name="employeeId">Employee ID.</param>
        /// <returns>Task completed.</returns>
        Task DeleteAsync(string employeeId);
    }
}
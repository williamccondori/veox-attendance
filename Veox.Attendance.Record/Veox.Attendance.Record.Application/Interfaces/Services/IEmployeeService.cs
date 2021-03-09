using System;
using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Models;

namespace Veox.Attendance.Record.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Create or update a employee.
        /// </summary>
        /// <param name="employeeModel">Employee model.</param>
        /// <returns>Task completed.</returns>
        Task SaveAsync(EmployeeModel employeeModel);
        
        
        /// <summary>
        /// Delete a employee.
        /// </summary>
        /// <param name="employeeId">Employee ID.</param>
        /// <returns>Task completed.</returns>
        Task DeleteAsync(string employeeId);
    }
}
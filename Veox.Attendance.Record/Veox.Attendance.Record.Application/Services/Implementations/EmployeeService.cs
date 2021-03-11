using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Services.Implementations.Common;
using Veox.Attendance.Record.Application.Services.Interfaces;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Domain.Repositories;

namespace Veox.Attendance.Record.Application.Services.Implementations
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task SaveAsync(EmployeeRequest employeeRequest)
        {
            var existingEmployee = await _employeeRepository.GetById(employeeRequest.Id);

            if (existingEmployee == null)
            {
                var newEmployee = EmployeeEntity.Create(employeeRequest.Id, employeeRequest.WorkspaceId,
                    employeeRequest.Name, employeeRequest.LastName, employeeRequest.DocumentNumber,
                    employeeRequest.ImageProfile, employeeRequest.IsEnabled, string.Empty);

                await _employeeRepository.Create(newEmployee);
            }
            else
            {
                existingEmployee.Name = employeeRequest.Name;
                existingEmployee.LastName = employeeRequest.LastName;
                existingEmployee.IsEnabled = employeeRequest.IsEnabled;
                existingEmployee.ImageProfile = employeeRequest.ImageProfile;
                existingEmployee.Update(string.Empty);

                await _employeeRepository.Update(existingEmployee.Id, existingEmployee);
            }
        }

        public async Task DeleteAsync(string employeeId)
        {
            var existingEmployee = await _employeeRepository.GetById(employeeId);

            existingEmployee.Delete(string.Empty);

            await _employeeRepository.Update(existingEmployee.Id, existingEmployee);
        }
    }
}
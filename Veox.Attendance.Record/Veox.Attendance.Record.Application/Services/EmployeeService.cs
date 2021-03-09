using System;
using System.Threading.Tasks;
using Veox.Attendance.Record.Application.Interfaces.Services;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Domain.Entities;
using Veox.Attendance.Record.Domain.Repositories;

namespace Veox.Attendance.Record.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task SaveAsync(EmployeeModel employeeModel)
        {
            var existingEmployee = await _employeeRepository.GetById(employeeModel.Id);

            if (existingEmployee == null)
            {
                var newEmployee = EmployeeEntity.Create(employeeModel.Id, employeeModel.Name, employeeModel.LastName,
                    employeeModel.DocumentNumber, employeeModel.ImageProfile, employeeModel.IsEnabled, string.Empty);

                await _employeeRepository.Create(newEmployee);
            }
            else
            {
                existingEmployee.Name = employeeModel.Name;
                existingEmployee.LastName = employeeModel.LastName;
                existingEmployee.IsEnabled = employeeModel.IsEnabled;
                existingEmployee.ImageProfile = employeeModel.ImageProfile;
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
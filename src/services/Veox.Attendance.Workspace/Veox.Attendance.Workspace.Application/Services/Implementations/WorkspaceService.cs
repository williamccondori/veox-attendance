using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Application.Contexts.Interfaces;
using Veox.Attendance.Workspace.Application.Exceptions;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Services.Implementations.Common;
using Veox.Attendance.Workspace.Application.Services.Interfaces;
using Veox.Attendance.Workspace.Application.Validators;
using Veox.Attendance.Workspace.Domain.Constants;
using Veox.Attendance.Workspace.Domain.Entities;
using Veox.Attendance.Workspace.Domain.Repositories;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Identity;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Models.Record;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Producers.Identity;
using Veox.Attendance.Workspace.Infraestructure.RabbitMq.Producers.Record;

namespace Veox.Attendance.Workspace.Application.Services.Implementations
{
    public class WorkspaceService : BaseService, IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IIdentityProducer _identityProducer;
        private readonly IRecordProducer _recordProducer;

        public WorkspaceService(
            IApplicationContext context,
            IWorkspaceRepository workspaceRepository,
            IEmployeeRepository employeeRepository,
            IGroupRepository groupRepository,
            IIdentityProducer identityProducer,
            IRecordProducer recordProducer) : base(context)
        {
            _workspaceRepository = workspaceRepository;
            _employeeRepository = employeeRepository;
            _groupRepository = groupRepository;
            _identityProducer = identityProducer;
            _recordProducer = recordProducer;
        }

        public async Task<List<WorkspaceResponse>> GetAsync()
        {
            if (string.IsNullOrEmpty(_context.UserId))
                throw new KeyNotFoundException();

            var employees = await _employeeRepository.GetAllByUserId(_context.UserId);

            var workspaceResponses = new List<WorkspaceResponse>();

            foreach (var employee in employees.Where(x => x.IsEnabled))
            {
                var workspace = await _workspaceRepository.GetById(employee.WorkspaceId);
                workspaceResponses.Add(new WorkspaceResponse
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    Description = workspace.Description,
                    ImageProfile = workspace.ImageProfile
                });
            }

            return workspaceResponses;
        }

        public async Task<WorkspaceResponse> CreateAsync(WorkspaceRequest workspaceRequest)
        {
            Validate(new WorkspaceRequestValidator(), workspaceRequest);

            // Workspace validation.

            var workspace = await _workspaceRepository.GetByIdentifier(workspaceRequest.Identifier);

            if (workspace != null)
                throw new ApiException($"The {workspace.Identifier} workspace already exists.");

            // Generate image profile.

            var initials = workspaceRequest.Identifier.Substring(0, 2);

            var imageProfile = GetImageProfile(initials.ToUpper());

            // Workspace creation.

            var newWorkspace = WorkspaceEntity.Create(workspaceRequest.Name, workspaceRequest.Identifier,
                workspaceRequest.Description, imageProfile, _context.UserId);

            newWorkspace = await _workspaceRepository.Create(newWorkspace);

            // Group creation.

            var newGroup = GroupEntity.Create(GroupType.DEFAULT, _context.UserId);

            newGroup = await _groupRepository.Create(newGroup);

            // Employee creation.

            var actualUser = workspaceRequest.User;

            var newEmployee = EmployeeEntity.Create(actualUser.Name, actualUser.LastName, actualUser.Email,
                workspaceRequest.DocumentNumber, newWorkspace.Id, workspaceRequest.TotalHours, actualUser.ImageProfile,
                newGroup.Id, _context.UserId);

            newEmployee = await _employeeRepository.Create(newEmployee);

            // Register the new user role.

            _identityProducer.AddUserRol(new AddUserRolModel
            {
                WorkspaceId = newWorkspace.Id,
                EmployeeId = newEmployee.Id,
                UserId = _context.UserId
            });


            // Register the new employee.

            _recordProducer.AddEmployee(new AddEmployeeModel
            {
                EmployeeId = newEmployee.Id,
                WorkspaceId = newEmployee.WorkspaceId,
                Name = newEmployee.Name,
                LastName = newEmployee.LastName,
                DocumentNumber = newEmployee.DocumentNumber,
                TotalHours = newEmployee.TotalHours,
                IsEnabled = newEmployee.IsEnabled,
                ImageProfile = newEmployee.ImageProfile
            });

            return new WorkspaceResponse
            {
                Id = newWorkspace.Id,
                Name = newWorkspace.Name,
                Description = newWorkspace.Description,
                ImageProfile = newWorkspace.ImageProfile,
                EmployeeId = newEmployee.Id
            };
        }

        public Task<WorkspaceResponse> UpdateAsync(string workspaceId, WorkspaceRequest workspaceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> AddEmployeeAsync(string workspaceId, EmployeeRequest employeeRequest)
        {
            // Employee creation.

            /**
            var workspace = null;

            var newEmployee = EmployeeEntity.Create(actualUser.Name, actualUser.LastName, actualUser.Email,
                employeeRequest.DocumentNumber, newWorkspace.Id, workspaceRequest.TotalHours, actualUser.ImageProfile,
                newGroup.Id, _context.UserId);

            newEmployee = await _employeeRepository.Create(newEmployee);


            _recordProducer.AddEmployee(new AddEmployeeModel
            {
                EmployeeId = newEmployee.Id,
                WorkspaceId = newEmployee.WorkspaceId,
                Name = newEmployee.Name,
                LastName = newEmployee.LastName,
                DocumentNumber = newEmployee.DocumentNumber,
                TotalHours = newEmployee.TotalHours,
                IsEnabled = newEmployee.IsEnabled,
                ImageProfile = newEmployee.ImageProfile
            });
            
            return **/

            throw new NotImplementedException();
        }
    }
}
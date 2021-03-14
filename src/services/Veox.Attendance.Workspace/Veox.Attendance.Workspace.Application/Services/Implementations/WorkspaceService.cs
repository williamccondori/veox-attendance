using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veox.Attendance.Workspace.Application.Exceptions;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Services.Implementations.Common;
using Veox.Attendance.Workspace.Application.Services.Interfaces;
using Veox.Attendance.Workspace.Application.Validators;
using Veox.Attendance.Workspace.Domain.Entities;
using Veox.Attendance.Workspace.Domain.Repositories;

namespace Veox.Attendance.Workspace.Application.Services.Implementations
{
    public class WorkspaceService : BaseService, IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspaceService(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        
        public async Task<List<WorkspaceResponse>> GetAsync()
        {
            const string employeeId = "6047e634792b138a0fd82385";
            
            var workspaceEntities = await _workspaceRepository.GetByEmployee(employeeId);
            var workspaces = workspaceEntities.ToList();

            return workspaces.Select(workspaceEntity => new WorkspaceResponse
            {
                Id = workspaceEntity.Id,
                Name = workspaceEntity.Name,
                Description = workspaceEntity.Description,
                ImageProfile = workspaceEntity.ImageProfile
            }).ToList();
        }

        public async Task<WorkspaceResponse> CreateAsync(WorkspaceRequest workspaceRequest)
        {
            Validate(new WorkspaceRequestValidator(),workspaceRequest);

            var workspace = await _workspaceRepository.GetByIdentifier(workspaceRequest.Identifier);

            if (workspace != null)
            {
                throw new ApiException($"The {workspace.Identifier} workspace already exists.");
            }

            // Generate image profile.
            
            var initials = workspaceRequest.Identifier.Substring(0, 2);
            var imageProfile = GetImageProfile(initials.ToUpper());

            // Save the entity.
            
            var newWorkspace = WorkspaceEntity.Create(workspaceRequest.Name, workspaceRequest.Identifier,
                workspaceRequest.Description, imageProfile, string.Empty);
            newWorkspace = await _workspaceRepository.Create(newWorkspace);

            var workspaceResponse = new WorkspaceResponse
            {
                Id = newWorkspace.Id,
                Name = newWorkspace.Name,
                Description = newWorkspace.Description,
                ImageProfile = newWorkspace.ImageProfile
            };

            return workspaceResponse;
        }

        public Task<WorkspaceResponse> UpdateAsync(string workspaceId, WorkspaceRequest workspaceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> AddEmployeeAsync(string workspaceId, EmployeeRequest employeeRequest)
        {
            throw new NotImplementedException();
        }
    }
}
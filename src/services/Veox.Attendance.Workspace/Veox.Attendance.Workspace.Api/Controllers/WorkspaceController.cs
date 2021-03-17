using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Services.Interfaces;
using Veox.Attendance.Workspace.Application.Wrappers;

namespace Veox.Attendance.Workspace.Api.Controllers
{
    /// <summary>
    /// Workspace controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WorkspaceController : ControllerBase
    {
        private readonly ILogger<WorkspaceController> _logger;
        private readonly IWorkspaceService _workspaceService;

        /// <summary>
        /// Record controller.
        /// </summary>
        /// <param name="logger">Logger service.</param>
        /// <param name="workspaceService">Workspace service.</param>
        public WorkspaceController(
            ILogger<WorkspaceController> logger,
            IWorkspaceService workspaceService)
        {
            _logger = logger;
            _workspaceService = workspaceService;
        }

        /// <summary>
        /// Get all workspaces of current user.
        /// </summary>
        /// <returns>List of workspaces of current user.</returns>
        [HttpGet]
        public async Task<ActionResult<Response<List<WorkspaceResponse>>>> Get()
        {
            _logger.LogTrace(@"Executing: {Method}", nameof(Get));

            var result = await _workspaceService.GetAsync();

            return Ok(new Response<List<WorkspaceResponse>>(result));
        }

        /// <summary>
        /// Add a new workspace.
        /// </summary>
        /// <param name="workspaceRequest">Workspace request model.</param>
        /// <returns>Workspace created response model.</returns>
        [HttpPost]
        public async Task<ActionResult<Response<WorkspaceResponse>>> Create(
            [FromBody] WorkspaceRequest workspaceRequest)
        {
            _logger.LogTrace(@"Executing: {Method}", nameof(Create));

            var result = await _workspaceService.CreateAsync(workspaceRequest);

            return Created(nameof(Create), new Response<WorkspaceResponse>(result));
        }

        /// <summary>
        /// Update an existing workspace.
        /// </summary>
        /// <param name="workspaceId">Workspace ID.</param>
        /// <param name="workspaceRequest">Workspace request model.</param>
        /// <returns>Workspace updated response model.</returns>
        [HttpPut("{workspaceId}")]
        public async Task<ActionResult<WorkspaceResponse>> Update(string workspaceId,
            [FromBody] WorkspaceRequest workspaceRequest)
        {
            _logger.LogTrace(@"Executing: {Method}", nameof(Update));

            var result = await _workspaceService.UpdateAsync(workspaceId, workspaceRequest);

            return Ok(new Response<WorkspaceResponse>(result));
        }

        /// <summary>
        /// Send a invitation to new employee.
        /// </summary>
        /// <param name="workspaceId">Workspace ID.</param>
        /// <param name="employeeRequest">Employee request model.</param>
        /// <returns>Employee response model.</returns>
        [HttpPost("{workspaceId}/employees")]
        public async Task<ActionResult<EmployeeResponse>> AddEmployee(string workspaceId,
            EmployeeRequest employeeRequest)
        {
            _logger.LogTrace(@"Executing: {Method}", nameof(AddEmployee));

            var result = await _workspaceService.AddEmployeeAsync(workspaceId, employeeRequest);

            return Ok(new Response<EmployeeResponse>(result));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veox.Attendance.Workspace.Application.Models;
using Veox.Attendance.Workspace.Application.Services;
using Veox.Attendance.Workspace.Application.Wrappers;

namespace Veox.Attendance.Workspace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkspacesController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspacesController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }
        
        /// <summary>
        /// Get all workspaces.
        /// </summary>
        /// <returns>List of workspaces of current user.</returns>
        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<WorkspaceResponse>>>> GetByUser()
        {
            var response = await _workspaceService.GetByUserAsync();

            return Ok(response);
        }

        /// <summary>
        /// Create a new workspace.
        /// </summary>
        /// <param name="workspace"> Workspace model.</param>
        /// <returns>Workspace model created.</returns>
        [HttpPost]
        public async Task<ActionResult<Response<WorkspaceResponse>>> Create([FromBody] WorkspaceModel workspace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var response = await _workspaceService.CreateAsync(workspace);

            return Created(nameof(Create), response);
        }

        /// <summary>
        /// Update a existing workspace.
        /// </summary>
        /// <param name="id">Workspace ID.</param>
        /// <param name="workspace">Workspace model.</param>
        /// <returns>Workspace model updated.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkspaceResponse>> Update(Guid id, [FromBody] WorkspaceModel workspace)
        {
            if (Guid.Empty == id)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _workspaceService.UpdateAsync(workspace);

            return Ok(response);
        }
    }
}
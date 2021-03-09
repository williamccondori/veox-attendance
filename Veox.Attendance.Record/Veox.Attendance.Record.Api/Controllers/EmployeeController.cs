using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veox.Attendance.Record.Application.Interfaces.Services;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Wrappers;

namespace Veox.Attendance.Record.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Response<EmployeeModel>>> Save([FromBody] EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _employeeService.SaveAsync(employeeModel);

            return Created(nameof(Save), employeeModel);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Record.Api.Controllers.Common;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Services.Interfaces;

namespace Veox.Attendance.Record.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : BaseController<EmployeeController>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            IEmployeeService employeeService,
            ILogger<EmployeeController> logger) : base(logger)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest employeeRequest)
        {
            await _employeeService.SaveAsync(employeeRequest);

            return Ok();
        }
    }
}
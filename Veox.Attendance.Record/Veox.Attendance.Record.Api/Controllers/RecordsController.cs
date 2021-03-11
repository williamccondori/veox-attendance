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
    public class RecordsController : BaseController<RecordsController>
    {
        private readonly IRecordService _recordService;

        protected RecordsController(
            IRecordService recordService,
            ILogger<RecordsController> logger) : base(logger)
        {
            _recordService = recordService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecordCreateRequest recordCreateRequest)
        {
            return await InvokeService(_recordService, x => x.CreateAsync(recordCreateRequest), nameof(Created));
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummaryByEmployee([FromQuery] SummaryEmployeeRequest summaryEmployeeRequest)
        {
            return await InvokeService(_recordService, x => x.GetSummaryByEmployeeAsync(summaryEmployeeRequest));
        }

        [HttpGet("summary/daily")]
        public async Task<IActionResult> GetDailySummary([FromQuery] DailySummaryRequest dailySummaryRequest)
        {
            return await InvokeService(_recordService, x => x.GetDailySummaryAsync(dailySummaryRequest));
        }
    }
}
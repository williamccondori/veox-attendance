using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Record.Api.Controllers.Common;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Services.Interfaces;
using Veox.Attendance.Record.Application.Wrappers;

namespace Veox.Attendance.Record.Api.Controllers
{
    /// <summary>
    /// Record controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RecordsController : BaseController<RecordsController>
    {
        private readonly IRecordService _recordService;

        /// <summary>
        /// Record controller.
        /// </summary>
        /// <param name="recordService">Record service.</param>
        /// <param name="logger">Logger service.</param>
        public RecordsController(
            IRecordService recordService,
            ILogger<RecordsController> logger) : base(logger)
        {
            _recordService = recordService;
        }

        /// <summary>
        /// Create a new record.
        /// </summary>
        /// <param name="recordCreateRequest">Record create request.</param>
        /// <returns>Summary by employee response.</returns>
        [HttpPost]
        public async Task<ActionResult<Response<SummaryEmployeeResponse>>> Create(
            [FromBody] RecordCreateRequest recordCreateRequest)
        {
            _logger.LogTrace("Excecuting <{MethodName}>", nameof(Create));

            var result = await _recordService.CreateAsync(recordCreateRequest);

            return Created(nameof(Created), new Response<SummaryEmployeeResponse>(result));
        }

        /// <summary>
        /// Get the record summary by employee.
        /// </summary>
        /// <param name="summaryEmployeeRequest">Summary by employee request.</param>
        /// <returns>Summary by employee response.</returns>
        [HttpGet("summary")]
        public async Task<ActionResult<Response<SummaryEmployeeResponse>>> GetSummaryByEmployee(
            [FromQuery] SummaryEmployeeRequest summaryEmployeeRequest)
        {
            _logger.LogTrace("Excecuting <{MethodName}>", nameof(GetSummaryByEmployee));

            var result = await _recordService.GetSummaryByEmployeeAsync(summaryEmployeeRequest);

            return Ok(new Response<SummaryEmployeeResponse>(result));
        }

        /// <summary>
        /// Get the daily record summary by workspace.
        /// </summary>
        /// <param name="dailySummaryRequest">Daily summary request.</param>
        /// <returns>Daily summary response.</returns>
        [HttpGet("summary/daily")]
        public async Task<ActionResult<Response<List<DailySummaryResponse>>>> GetDailySummary(
            [FromQuery] DailySummaryRequest dailySummaryRequest)
        {
            _logger.LogTrace("Excecuting <{MethodName}>", nameof(GetDailySummary));

            var result = await _recordService.GetDailySummaryAsync(dailySummaryRequest);

            return Ok(new Response<List<DailySummaryResponse>>(result));
        }
    }
}
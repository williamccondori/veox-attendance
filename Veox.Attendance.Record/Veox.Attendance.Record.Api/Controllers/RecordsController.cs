using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veox.Attendance.Record.Application.Interfaces.Services;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Wrappers;

namespace Veox.Attendance.Record.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecordCreateRequest recordCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _recordService.CreateAsync(recordCreateRequest);
            
            return Created(nameof(Create), new Response<SummaryEmployeeResponse>(result));
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummaryByEmployee([FromQuery] SummaryEmployeeRequest summaryEmployeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _recordService.GetSummaryByEmployeeAsync(summaryEmployeeRequest);

            return Ok(new Response<SummaryEmployeeResponse>(result));
        }

        [HttpGet("summary/daily")]
        public async Task<IActionResult> GetDailySummary([FromQuery] DailySummaryRequest dailySummaryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _recordService.GetDailySummaryAsync(dailySummaryRequest);

            return Ok(new Response<List<DailySummaryResponse>>(result));
        }
    }
}
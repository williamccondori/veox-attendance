using System.Collections;
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
        public async Task<ActionResult<Response<RecordModel>>> Create([FromBody] RecordCreateModel recordCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _recordService.CreateAsync(recordCreateModel);

            return Created(nameof(Create), response);
        }

        [HttpGet("summary/daily")]
        public async Task<ActionResult<Response<IEnumerable>>> GetDailySummaryByWorkspace(
            [FromQuery] DailySummaryRequest dailySummaryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _recordService.GetDailySummaryByWorkspace(dailySummaryRequest);

            return Ok(response);
        }
    }
}
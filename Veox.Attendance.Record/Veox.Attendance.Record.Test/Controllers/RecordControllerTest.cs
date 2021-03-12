using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Veox.Attendance.Record.Api.Controllers;
using Veox.Attendance.Record.Application.Models;
using Veox.Attendance.Record.Application.Services.Interfaces;
using Veox.Attendance.Record.Application.Wrappers;
using Xunit;

namespace Veox.Attendance.Record.Test.Controllers
{
    public class RecordControllerTest
    {
        private readonly Mock<IRecordService> _mockRecordService;

        public RecordControllerTest()
        {
            _mockRecordService = new Mock<IRecordService>(MockBehavior.Default);
        }

        private RecordsController CreateRecordsController()
        {
            return new RecordsController(_mockRecordService.Object,
                NullLogger<RecordsController>.Instance);
        }

        [Fact]
        public async Task RecordController_GetSummaryByEmployee_Ok()
        {
            var controller = CreateRecordsController();

            var actionResult = await controller.GetSummaryByEmployee(null);

            Assert.IsType<ActionResult<Response<SummaryEmployeeResponse>>>(actionResult);
        }

        [Fact]
        public async Task RecordController_GetDailySummary_Ok()
        {
            var controller = CreateRecordsController();

            var actionResult = await controller.GetDailySummary(null);

            Assert.IsType<ActionResult<Response<List<DailySummaryResponse>>>>(actionResult);
        }

        [Fact]
        public async Task RecordController_Created_Ok()
        {
            var controller = CreateRecordsController();

            var actionResult = await controller.Create(null);

            Assert.IsType<ActionResult<Response<SummaryEmployeeResponse>>>(actionResult);
        }
    }
}
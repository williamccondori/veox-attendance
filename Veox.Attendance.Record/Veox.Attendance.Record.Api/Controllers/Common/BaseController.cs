using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Record.Application.Services.Interfaces.Common;
using Veox.Attendance.Record.Application.Wrappers;

namespace Veox.Attendance.Record.Api.Controllers.Common
{
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly ILogger<T> _logger;

        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected async Task<ActionResult> InvokeService<TService, TOperation>(TService service,
            Func<TService, Task<TOperation>> operation) where TService : IBaseService
        {
            _logger.LogTrace($"Executing <{nameof(service)}|{nameof(operation)}>");

            var result = await operation.Invoke(service);

            return Ok(new Response<TOperation>(result));
        }

        protected async Task<ActionResult> InvokeService<TService, TOperation>(TService service,
            Func<TService, Task<TOperation>> operation, string functionName) where TService : IBaseService
        {
            _logger.LogTrace($"Executing <{nameof(service)}|{nameof(operation)}>");

            var result = await operation.Invoke(service);

            return Created(functionName, new Response<TOperation>(result));
        }
    }
}
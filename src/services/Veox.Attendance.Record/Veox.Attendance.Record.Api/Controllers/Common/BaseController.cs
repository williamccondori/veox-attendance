using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Veox.Attendance.Record.Api.Controllers.Common
{
    /// <summary>
    /// Base controller.
    /// </summary>
    /// <typeparam name="T">Type of controller.</typeparam>
    public class BaseController<T> : ControllerBase where T : class
    {
        /// <summary>
        /// Logger service.
        /// </summary>
        protected readonly ILogger<T> _logger;

        /// <summary>
        /// Base controller.
        /// </summary>
        /// <param name="logger">Logger service.</param>
        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
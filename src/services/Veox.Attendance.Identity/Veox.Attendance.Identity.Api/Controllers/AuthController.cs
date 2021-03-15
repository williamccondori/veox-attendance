using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Identity.Api.Controllers.Common;
using Veox.Attendance.Identity.Application.Services.Interfaces;

namespace Veox.Attendance.Identity.Api.Controllers
{
    /// <summary>
    /// Auth controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;
        
        /// <summary>
        /// Auth controller.
        /// </summary>
        /// <param name="authService">Auth service.</param>
        /// <param name="logger">Logger service.</param>
        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger) : base(logger)
        {
            _authService = authService;
        }
    }
}
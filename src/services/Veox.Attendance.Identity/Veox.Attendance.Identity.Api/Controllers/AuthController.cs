using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Identity.Api.Controllers.Common;
using Veox.Attendance.Identity.Application.Models;
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
        
        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="authenticationRequest">Authentication request model.</param>
        /// <returns>Authentication response model.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate(
            [FromBody] AuthenticationRequest authenticationRequest)
        {
            LogTrace(nameof(Authenticate));

            var result = await _authService.AuthenticateAsync(authenticationRequest);

            return Ok(result);
        }
    }
}
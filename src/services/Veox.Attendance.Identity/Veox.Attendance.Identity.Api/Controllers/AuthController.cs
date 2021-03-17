using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Interfaces;

namespace Veox.Attendance.Identity.Api.Controllers
{
    /// <summary>
    /// Auth controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        
        /// <summary>
        /// Auth controller.
        /// </summary>
        /// <param name="logger">Logger service.</param>
        /// <param name="authService">Auth service.</param>
        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
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
            _logger.LogTrace(@"Executing: {Method}", nameof(Authenticate));

            var result = await _authService.AuthenticateAsync(authenticationRequest);

            return Ok(result);
        }
    }
}
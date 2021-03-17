using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Interfaces;
using Veox.Attendance.Identity.Application.Wrappers;

namespace Veox.Attendance.Identity.Api.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        /// <summary>
        /// Auth controller.
        /// </summary>
        /// <param name="logger">Logger service.</param>
        /// <param name="userService">User service.</param>
        public UserController(
            ILogger<UserController> logger,
            IUserService userService)

        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="registerRequest">Register request model.</param>
        /// <returns>Register response model.</returns>
        [HttpPost]
        public async Task<ActionResult<Response<RegisterResponse>>> Register(RegisterRequest registerRequest)
        {
            _logger.LogTrace(@"Executing: {Method}", nameof(Register));
            
            var result = await _userService.RegisterAsync(registerRequest);
            
            return Created(nameof(Register), result);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.User.Application.Models;
using Veox.Attendance.User.Application.Services.Interfaces;

namespace Veox.Attendance.User.Api.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        /// <summary>
        /// Auth controller.
        /// </summary>
        /// <param name="logger">Logger service.</param>
        /// <param name="userService">User service.</param>
        public UsersController(
            ILogger<UsersController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get user information.
        /// </summary>
        /// <returns>User response model.</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResponse>> Get()
        {
            _logger.LogTrace(@"Executing: {Method}", nameof(Get));

            var result = await _userService.GetAsync();

            return Ok(result);
        }
    }
}
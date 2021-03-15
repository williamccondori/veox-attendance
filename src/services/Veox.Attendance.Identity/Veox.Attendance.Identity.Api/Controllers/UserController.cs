using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Identity.Api.Controllers.Common;
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
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Auth controller.
        /// </summary>
        /// <param name="userService">User service.</param>
        /// <param name="logger">Logger service.</param>
        public UserController(
            IUserService userService,
            ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="registerRequest">User request model.</param>
        /// <returns>User created response model.</returns>
        [HttpPost]
        public async Task<ActionResult<Response<RegisterResponse>>> Create(
            [FromBody] RegisterRequest registerRequest)
        {
            LogTrace(nameof(Create));

            var result = await _userService.RegisterAsync(registerRequest);

            return Created(nameof(Create), new Response<RegisterResponse>(result));
        }
    }
}
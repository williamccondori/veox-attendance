using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Record.Application.Contexts.Interfaces;

// ReSharper disable UnusedType.Local

namespace Veox.Attendance.Record.Api.Attributes
{
    /// <summary>
    /// Generate session attribute.
    /// </summary>
    public class GenerateSessionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Generate session attribute.
        /// </summary>
        public GenerateSessionAttribute() : base(
            typeof(GenerateSessionFilter))
        {
        }
    }

    /// <summary>
    /// Generate session filter.
    /// </summary>
    public class GenerateSessionFilter : IActionFilter
    {
        private readonly ILogger<GenerateSessionFilter> _logger;
        private readonly IApplicationContext _applicationContext;

        /// <summary>
        /// Generate session filter.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="applicationContext"></param>
        public GenerateSessionFilter(
            ILogger<GenerateSessionFilter> logger,
            IApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// On action executing.
        /// </summary>
        /// <param name="context">Context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var headerValues))
                return;

            try
            {
                var tokenString = headerValues.ToString()?.Substring(7) ?? string.Empty;

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                if (!(jwtSecurityTokenHandler.ReadToken(tokenString) is JwtSecurityToken token))
                    throw new AuthenticationException();

                var userId = token.Claims.Single(x => x.Type == "userId").Value;

                _applicationContext.Update(userId);
            }
            catch (Exception exception)
            {
                _logger.LogError(@"{Message}", exception.Message);
            }
        }

        /// <summary>
        /// On action executed.
        /// </summary>
        /// <param name="context">Context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
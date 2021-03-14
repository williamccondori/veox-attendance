using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Veox.Attendance.Workspace.Application.Exceptions;
using Veox.Attendance.Workspace.Application.Wrappers;

namespace Veox.Attendance.Workspace.Api.Middlewares
{
    /// <summary>
    /// Error handler middleware.
    /// </summary>
    /// ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke middleware.
        /// </summary>
        /// <param name="httpContext">Http context.</param>
        /// <returns>Task finished.</returns>
        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                var responseModel = new Response<string>(exception.Message);

                switch (exception)
                {
                    case ApiException _:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;

                    case ApiValidationException validationException:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        responseModel.Errors = validationException.Errors;
                        break;

                    case KeyNotFoundException _:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                
                var result = JsonSerializer.Serialize(responseModel, serializeOptions);

                await response.WriteAsync(result);
            }
        }
    }
}
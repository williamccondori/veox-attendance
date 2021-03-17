using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Veox.Attendance.Workspace.Application.Exceptions;
using Veox.Attendance.Workspace.Application.Wrappers;

namespace Veox.Attendance.Workspace.Api.Attributes
{
    /// <summary>
    /// Error handler attribute.
    /// </summary>
    public class ErrorHandlerAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Error handler attribute.
        /// </summary>
        public ErrorHandlerAttribute() : base(
            typeof(ErrorHandlerFilter))
        {
        }

        private class ErrorHandlerFilter : ExceptionFilterAttribute
        {
            public override void OnException(ExceptionContext exceptionContext)
            {
                int statusCode;
                
                var exception = exceptionContext.Exception;
                
                var responseModel = new Response<string>(exception.Message);

                switch (exception)
                {
                    case ApiException _:
                        statusCode = (int) HttpStatusCode.BadRequest;
                        break;

                    case ApiValidationException validationException:
                        statusCode= (int) HttpStatusCode.BadRequest;
                        responseModel.Errors = validationException.Errors;
                        break;

                    case KeyNotFoundException _:
                        statusCode= (int) HttpStatusCode.NotFound;
                        break;

                    default:
                        statusCode= (int) HttpStatusCode.InternalServerError;
                        break;
                }

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                
                var result = JsonSerializer.Serialize(responseModel, serializeOptions);

                exceptionContext.HttpContext.Response.StatusCode = statusCode;
                exceptionContext.HttpContext.Response.WriteAsync(result);
            }
        }
    }
}
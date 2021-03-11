using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Veox.Attendance.Record.Application.Exceptions;
using Veox.Attendance.Record.Application.Wrappers;

namespace Veox.Attendance.Record.Api.Middlewares
{
    /// <summary>
    /// Execute the error handler in the application.
    /// </summary>
    public abstract class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        protected ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new Response<string>(exception.Message);

                switch (exception)
                {
                    case ApiException _:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;

                    case ValidationException e:
                        response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
                        responseModel.Errors = e.Errors;
                        break;

                    case KeyNotFoundException _:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;

                    case NotImplementedException _:
                        responseModel.Message = "Esta característica aún no está disponible";
                        response.StatusCode = (int) HttpStatusCode.NotImplemented;
                        break;

                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veox.Attendance.Record.Application.Exceptions;
using Veox.Attendance.Record.Application.Services.Interfaces.Common;
using Veox.Attendance.Record.Application.Wrappers;

namespace Veox.Attendance.Record.Api.Controllers.Common
{
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly ILogger<T> _logger;

        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected async Task<ActionResult> InvokeService<TService, TOperation>(TService service,
            Func<TService, Task<TOperation>> operation) where TService : IBaseService
        {
            try
            {
                _logger.LogTrace($"Executing <{nameof(service)}|{nameof(operation)}>");

                var result = await operation.Invoke(service);

                return Ok(new Response<TOperation>(result));
            }
            catch (Exception exception)
            {
                var responseModel = new Response<string>(exception.Message);

                switch (exception)
                {
                    case ApiException _:
                        return StatusCode(StatusCodes.Status400BadRequest, responseModel);

                    case ValidationException validationException:
                        responseModel.Errors = validationException.Errors;
                        return StatusCode(StatusCodes.Status400BadRequest, responseModel);

                    case KeyNotFoundException _:
                        return StatusCode(StatusCodes.Status404NotFound, responseModel);

                    case NotImplementedException _:
                        responseModel.Message = "Esta característica aún no está disponible";
                        return StatusCode(StatusCodes.Status501NotImplemented, responseModel);

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }
            }
        }

        protected async Task<ActionResult> InvokeService<TService, TOperation>(TService service,
            Func<TService, Task<TOperation>> operation, string functionName) where TService : IBaseService
        {
            try
            {
                _logger.LogTrace($"Executing <{nameof(service)}|{nameof(operation)}>");

                var result = await operation.Invoke(service);

                return Created(functionName, new Response<TOperation>(result));
            }
            catch (Exception exception)
            {
                var responseModel = new Response<string>(exception.Message);

                switch (exception)
                {
                    case ApiException _:
                        return StatusCode(StatusCodes.Status400BadRequest, responseModel);

                    case ValidationException validationException:
                        responseModel.Errors = validationException.Errors;
                        return StatusCode(StatusCodes.Status400BadRequest, responseModel);

                    case KeyNotFoundException _:
                        return StatusCode(StatusCodes.Status404NotFound, responseModel);

                    case NotImplementedException _:
                        responseModel.Message = "Esta característica aún no está disponible";
                        return StatusCode(StatusCodes.Status501NotImplemented, responseModel);

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }
            }
        }
    }
}
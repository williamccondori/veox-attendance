#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

#endregion

using System.Collections.Generic;
using Veox.Attendance.Record.Application.Exceptions;

namespace Veox.Attendance.Record.Application.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<ApiValidationException.ErrorModel> Errors { get; set; }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}
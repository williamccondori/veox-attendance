using System;
using System.Globalization;

namespace Veox.Attendance.Record.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
        }

        
        // ReSharper disable once UnusedMember.Global
        public ApiException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
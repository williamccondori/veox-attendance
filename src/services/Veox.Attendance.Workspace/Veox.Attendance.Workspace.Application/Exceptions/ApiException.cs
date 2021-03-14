#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

using System;
using System.Globalization;

namespace Veox.Attendance.Workspace.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

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
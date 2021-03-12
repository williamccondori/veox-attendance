using System;
using System.Collections.Generic;
using FluentValidation.Results;

#region ReSharper linter configuration.

// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace Veox.Attendance.Record.Application.Exceptions
{
    public class ApiValidationException : Exception
    {
        private ApiValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<ErrorModel>();
        }

        public List<ErrorModel> Errors { get; }

        public ApiValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(new ErrorModel
                {
                    PropertyName = failure.PropertyName,
                    ErrorMessage = failure.ErrorMessage
                });
            }
        }

        public class ErrorModel
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
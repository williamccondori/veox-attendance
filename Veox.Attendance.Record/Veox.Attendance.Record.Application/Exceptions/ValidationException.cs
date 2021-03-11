using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Veox.Attendance.Record.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<ErrorModel>();
        }

        public List<ErrorModel> Errors { get; set; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(new ErrorModel()
                {
                    PropertyName = failure.PropertyName,
                    ErrorMessage = failure.ErrorMessage,
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
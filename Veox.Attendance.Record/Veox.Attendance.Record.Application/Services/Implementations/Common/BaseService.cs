using System.Linq;
using FluentValidation;
using Veox.Attendance.Record.Application.Exceptions;
using Veox.Attendance.Record.Application.Services.Interfaces.Common;

namespace Veox.Attendance.Record.Application.Services.Implementations.Common
{
    public abstract class BaseService : IBaseService
    {
        protected static void Validate<TValidator, TModel>(TValidator validator, TModel model)
            where TValidator : AbstractValidator<TModel>
        {
            var result = validator.Validate(model);

            if (result.IsValid)
            {
                return;
            }

            var validationFailures = result.Errors.ToList();

            throw new ApiValidationException(validationFailures);
        }
    }
}
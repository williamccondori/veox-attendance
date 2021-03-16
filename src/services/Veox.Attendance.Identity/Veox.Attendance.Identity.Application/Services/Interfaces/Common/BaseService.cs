using System.Linq;
using FluentValidation;
using Veox.Attendance.Identity.Application.Exceptions;
using Veox.Attendance.Identity.Application.Services.Implementations.Common;

namespace Veox.Attendance.Identity.Application.Services.Interfaces.Common
{
    public class BaseService : IBaseService
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
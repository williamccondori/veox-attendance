using System.Linq;
using FluentValidation;
using Veox.Attendance.Record.Application.Services.Interfaces.Common;
using ValidationException = Veox.Attendance.Record.Application.Exceptions.ValidationException;

namespace Veox.Attendance.Record.Application.Services.Implementations.Common
{
    public abstract class BaseService : IBaseService
    {
        protected static async void Validate<TValidator, TModel>(TValidator validator, TModel model)
            where TValidator : AbstractValidator<TModel>
        {
            var result = await validator.ValidateAsync(model);

            if (result.IsValid)
                return;

            var errors = result.Errors.ToList();

            var errorModels = errors.Select(x => new ValidationException.ErrorModel
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName
            }).ToList();

            throw new ValidationException {Errors = errorModels};
        }
    }
}
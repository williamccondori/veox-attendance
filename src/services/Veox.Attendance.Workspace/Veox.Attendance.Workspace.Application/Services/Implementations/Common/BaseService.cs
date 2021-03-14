using System.Linq;
using FluentValidation;
using Veox.Attendance.Workspace.Application.Exceptions;
using Veox.Attendance.Workspace.Application.Helpers;

namespace Veox.Attendance.Workspace.Application.Services.Implementations.Common
{
    public class BaseService
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
    
        protected static string GetImageProfile(string initials)
        {
            var color = ColorHelper.Generate();
            
            var imageProfile = $"https://dummyimage.com/300x300/{color}/fff/&text=+{initials}+";

            return imageProfile;
        }
    }
}
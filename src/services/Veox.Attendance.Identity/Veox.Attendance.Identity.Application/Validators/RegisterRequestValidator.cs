using FluentValidation;
using Veox.Attendance.Identity.Application.Models;

namespace Veox.Attendance.Identity.Application.Validators
{
    public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            
        }
    }
}
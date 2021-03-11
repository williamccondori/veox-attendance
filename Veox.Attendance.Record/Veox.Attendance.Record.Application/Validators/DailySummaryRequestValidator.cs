using FluentValidation;
using Veox.Attendance.Record.Application.Models;

namespace Veox.Attendance.Record.Application.Validators
{
    public class DailySummaryRequestValidator : AbstractValidator<DailySummaryRequest>
    {
        public DailySummaryRequestValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("{PropertyName} es obligatorio.")
                .NotNull()
                .MaximumLength(50);
        }
    }
}
using FluentValidation;
using Veox.Attendance.Workspace.Application.Models;

namespace Veox.Attendance.Workspace.Application.Validators
{
    public class WorkspaceRequestValidator : AbstractValidator<WorkspaceRequest>
    {
        public WorkspaceRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Identifier)
                .NotEmpty()
                .NotNull()
                .MaximumLength(25)
                .Matches(@"\A\S+\z");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(300);
        }
    }
}
using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.ChangeLeaveStatus;

internal class ChangeLeaveStatusCommandValidator : AbstractValidator<ChangeLeaveStatusCommand>
{
    public ChangeLeaveStatusCommandValidator()
    {
        RuleFor(x => x.LeaveId)
            .NotEmpty()
            .WithMessage(EmployeeLeaveErrors.EmptyId.Message);

        RuleFor(x => x.NewStatus)
            .IsInEnum()
            .WithMessage(EmployeeLeaveErrors.InvalidStatus.Message);
    }
}

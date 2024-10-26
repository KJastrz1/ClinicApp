using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.UpdateLeave;

internal class UpdateEmployeeLeaveCommandValidator : AbstractValidator<UpdateEmployeeLeaveCommand>
{
    public UpdateEmployeeLeaveCommandValidator()
    {
        RuleFor(x => x.LeaveId)
            .NotEmpty()
            .WithMessage(EmployeeLeaveErrors.EmptyId.Message);

        RuleFor(x => x.Reason)
            .NotEmpty()
            .When(x => !string.IsNullOrWhiteSpace(x.Reason))
            .WithMessage(EmployeeLeaveErrors.ReasonErrors.Required.Message)
            .MaximumLength(LeaveReason.MaxLength)
            .WithMessage(EmployeeLeaveErrors.ReasonErrors.TooLong.Message);

        RuleFor(x => x.StartDate)
            .Must(startDate => startDate> DateTime.UtcNow)
            .When(x => x.StartDate.HasValue)
            .WithMessage(EmployeeLeaveErrors.DateInThePast.Message);

        RuleFor(x => x.EndDate)
            .Must(endDate => endDate> DateTime.UtcNow)
            .When(x => x.EndDate.HasValue)
            .WithMessage(EmployeeLeaveErrors.DateInThePast.Message);
    }
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.CreateEmployeeLeave;

internal class CreateEmployeeLeaveCommandValidator : AbstractValidator<CreateEmployeeLeaveCommand>
{
    public CreateEmployeeLeaveCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .WithMessage(EmployeeLeaveErrors.EmptyId.Message);

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(EmployeeLeaveErrors.ReasonErrors.Required.Message)
            .MaximumLength(LeaveReason.MaxLength)
            .WithMessage(EmployeeLeaveErrors.ReasonErrors.TooLong.Message);

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage(EmployeeLeaveErrors.EndBeforeStart.Message)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage(EmployeeLeaveErrors.DateInThePast.Message);

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage(EmployeeLeaveErrors.DateInThePast.Message);
    }
}

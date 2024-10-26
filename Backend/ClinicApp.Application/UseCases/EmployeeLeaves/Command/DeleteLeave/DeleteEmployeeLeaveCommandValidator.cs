using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.DeleteLeave;

public class DeleteEmployeeLeaveCommandValidator
    : AbstractValidator<DeleteEmployeeLeaveCommand>
{
    public DeleteEmployeeLeaveCommandValidator()
    {
        RuleFor(x => x.LeaveId)
            .NotEmpty()
            .WithMessage(DoctorErrors.EmptyId.Message);
    }
}

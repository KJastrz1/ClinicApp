using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.Actions.Doctors.Command.DeleteDoctorSchedule;

internal class DeleteDoctorScheduleCommandValidator : AbstractValidator<DeleteDoctorScheduleCommand>
{
    public DeleteDoctorScheduleCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage(DoctorErrors.EmptyId.Message);

        RuleFor(x => x.ScheduleId).GreaterThanOrEqualTo(1).WithMessage(ScheduleErrors.InvalidScheduleId.Message);
    }
}

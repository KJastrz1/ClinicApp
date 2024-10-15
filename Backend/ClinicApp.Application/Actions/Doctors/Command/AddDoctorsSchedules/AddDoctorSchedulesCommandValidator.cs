using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.Actions.Doctors.Command.AddDoctorsSchedules;

internal class AddDoctorSchedulesCommandValidator : AbstractValidator<AddDoctorSchedulesCommand>
{
    public AddDoctorSchedulesCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage(DoctorErrors.EmptyId.Message);

        RuleFor(x => x.Schedules)
            .Must(schedules => schedules != null && schedules.Count > 0)
            .WithMessage(ScheduleErrors.InvalidScheduleId.Message);
        
        RuleForEach(x => x.Schedules).ChildRules(schedule =>
        {
            schedule.RuleFor(x => x.EndTime)
                .Must((command, endTime) => endTime > command.StartTime)
                .WithMessage(ScheduleErrors.EndTimeBeforeStartTime.Message);

            schedule.RuleFor(x => x.VisitDuration)
                .Must(visitDuration => visitDuration > VisitDuration.MinDuration && visitDuration <= VisitDuration.MaxDuration)
                .WithMessage(ScheduleErrors.InvalidVisitDuration
                    .Message);
        });
    }
}

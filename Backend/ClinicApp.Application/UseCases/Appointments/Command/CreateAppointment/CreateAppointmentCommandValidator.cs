using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.Shared;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Appointments.Command.CreateAppointment;

internal class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage(AppointmentErrors.DoctorIdRequired.Message);

        RuleFor(x => x.PatientId)
            .NotEmpty()
            .WithMessage(AppointmentErrors.PatientIdRequired.Message);

        RuleFor(x => x.ClinicId)
            .NotEmpty()
            .WithMessage(AppointmentErrors.ClinicIdRequired.Message);

        RuleFor(x => x.StartDateTime)
            .Must(BeAValidStartDateTime)
            .WithMessage(AppointmentErrors.AppointmentInPast.Message);

        RuleFor(x => x.Notes)
            .MaximumLength(AppointmentNotes.MaxLength)
            .WithMessage(AppointmentErrors.NotesErrors.TooLong.Message);
    }

    private bool BeAValidStartDateTime(DateTime startDateTime)
    {
        Result<AppointmentStartDateTime> result = AppointmentStartDateTime.Create(startDateTime);
        return result.IsSuccess;
    }
}

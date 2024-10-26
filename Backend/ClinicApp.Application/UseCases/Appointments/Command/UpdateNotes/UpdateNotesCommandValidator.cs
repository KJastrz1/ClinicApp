using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Appointments.Command.UpdateNotes;

public class UpdateNotesCommandValidator : AbstractValidator<UpdateNotesCommand>
{
    public UpdateNotesCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty()
            .WithMessage(AppointmentErrors.EmptyId.Message);

        RuleFor(x => x.Notes)
            .NotEmpty()
            .WithMessage(AppointmentErrors.NotesErrors.Empty.Message)
            .MaximumLength(AppointmentNotes.MaxLength)
            .WithMessage(AppointmentErrors.NotesErrors.TooLong.Message);
    }
}

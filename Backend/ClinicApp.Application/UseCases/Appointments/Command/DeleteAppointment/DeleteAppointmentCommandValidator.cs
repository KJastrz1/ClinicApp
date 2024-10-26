using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Appointments.Command.DeleteAppointment;

public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
{
    public DeleteAppointmentCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty()
            .WithMessage(AppointmentErrors.EmptyId.Message);
    }
}

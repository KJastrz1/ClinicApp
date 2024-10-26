using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Patients.Command.DeletePatient;

public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
{
    public DeletePatientCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty()
            .WithMessage(PatientErrors.EmptyId.Message);
    }
}

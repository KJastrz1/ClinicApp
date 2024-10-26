using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Doctors.Command.DeleteDoctor;

public class DeleteDoctorCommandValidator : AbstractValidator<DeleteDoctorCommand>
{
    public DeleteDoctorCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage(DoctorErrors.EmptyId.Message);
    }
}

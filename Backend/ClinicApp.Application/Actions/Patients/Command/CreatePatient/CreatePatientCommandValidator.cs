using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.Actions.Patients.Command.CreatePatient;

internal class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(UserBaseErrors.FirstNameErrors.Empty.Message)
            .MaximumLength(FirstName.MaxLength)
            .WithMessage(UserBaseErrors.FirstNameErrors.TooLong.Message);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(UserBaseErrors.LastNameErrors.Empty.Message)
            .MaximumLength(LastName.MaxLength)
            .WithMessage(UserBaseErrors.LastNameErrors.TooLong.Message);

        RuleFor(x => x.SocialSecurityNumber)
            .NotEmpty()
            .WithMessage(PatientErrors.SocialSecurityNumberErrors.Empty.Message)
            .Must(value => SocialSecurityNumber.Create(value).IsSuccess)
            .WithMessage(PatientErrors.SocialSecurityNumberErrors.InvalidFormat.Message);

        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidFutureDate.Message)
            .Must(date => date.Year >= DateOfBirth.MinYear)
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidPastDate.Message);
    }
}

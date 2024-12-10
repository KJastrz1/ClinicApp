using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Patients.Command.CreatePatient;

internal class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(UserProfileErrors.FirstNameErrors.Empty.Message)
            .MaximumLength(FirstName.MaxLength)
            .WithMessage(UserProfileErrors.FirstNameErrors.TooLong.Message);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(UserProfileErrors.LastNameErrors.Empty.Message)
            .MaximumLength(LastName.MaxLength)
            .WithMessage(UserProfileErrors.LastNameErrors.TooLong.Message);

        RuleFor(x => x.SocialSecurityNumber)
            .NotEmpty()
            .WithMessage(PatientErrors.SocialSecurityNumberErrors.Empty.Message)
            .Must(value => SocialSecurityNumber.Create(value).IsSuccess)
            .WithMessage(PatientErrors.SocialSecurityNumberErrors.InvalidFormat.Message);

        RuleFor(x => x.DateOfBirth)
            .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidFutureDate.Message)
            .Must(date => date >= DateOfBirth.MinDate)
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidPastDate.Message);
    }
}

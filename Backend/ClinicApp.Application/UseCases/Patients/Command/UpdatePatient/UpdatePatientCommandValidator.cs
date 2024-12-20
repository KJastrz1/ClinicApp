using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Patients.Command.UpdatePatient;

internal class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty().WithMessage(PatientErrors.EmptyId.Message);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .When(x => x.FirstName != null)
            .WithMessage(UserProfileErrors.FirstNameErrors.Empty.Message)
            .MaximumLength(FirstName.MaxLength)
            .When(x => x.FirstName != null)
            .WithMessage(UserProfileErrors.FirstNameErrors.TooLong.Message);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .When(x => x.LastName != null)
            .WithMessage(UserProfileErrors.LastNameErrors.Empty.Message)
            .MaximumLength(LastName.MaxLength)
            .When(x => x.LastName != null)
            .WithMessage(UserProfileErrors.LastNameErrors.TooLong.Message);

        RuleFor(x => x.SocialSecurityNumber)
            .NotEmpty()
            .When(x => x.SocialSecurityNumber != null)
            .WithMessage(PatientErrors.SocialSecurityNumberErrors.Empty.Message)
            .Must(value => value == null || SocialSecurityNumber.Create(value).IsSuccess)
            .WithMessage(PatientErrors.SocialSecurityNumberErrors.InvalidFormat.Message);

        RuleFor(x => x.DateOfBirth)
            .Must(date => date == null || date <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidFutureDate.Message)
            .Must(date => date == null || date >= DateOfBirth.MinDate)
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidPastDate.Message);
    }
}

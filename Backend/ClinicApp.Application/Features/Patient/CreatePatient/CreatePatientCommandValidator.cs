using FluentValidation;
using ClinicApp.Application.Features.Patient.CreatePatient;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.User.ValueObjects;

namespace ClinicApp.Application.Features.Patient.CreatePatient;

internal class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(UserBaseErrors.EmailErrors.Empty.Message)
            .MaximumLength(Email.MaxLength)
            .WithMessage(UserBaseErrors.EmailErrors.TooLong.Message)
            .Must(value => Email.Create(value).IsSuccess)
            .WithMessage(UserBaseErrors.EmailErrors.InvalidFormat.Message);

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

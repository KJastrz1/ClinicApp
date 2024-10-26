using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Patients.Command.RegisterPatient;

public class RegisterPatientCommandValidator : AbstractValidator<RegisterPatientCommand>
{
    public RegisterPatientCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(AccountErrors.EmailErrors.Empty.Message)
            .EmailAddress()
            .WithMessage(AccountErrors.EmailErrors.InvalidFormat.Message);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(AccountErrors.PasswordErrors.Empty.Message)
            .MinimumLength(8)
            .WithMessage(AccountErrors.PasswordErrors.TooShort.Message)
            .Matches("[A-Z]")
            .WithMessage(AccountErrors.PasswordErrors.MissingUppercase.Message)
            .Matches("[a-z]")
            .WithMessage(AccountErrors.PasswordErrors.MissingLowercase.Message)
            .Matches("[0-9]")
            .WithMessage(AccountErrors.PasswordErrors.MissingDigit.Message)
            .Matches("[^a-zA-Z0-9]")
            .WithMessage(AccountErrors.PasswordErrors.MissingSpecialCharacter.Message);

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
            .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidFutureDate.Message)
            .Must(date => date >= DateOfBirth.MinDate)
            .WithMessage(PatientErrors.DateOfBirthErrors.InvalidPastDate.Message);
    }
}

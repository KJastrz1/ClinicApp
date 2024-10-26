using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Accounts.Command.RegisterAccount;

public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidator()
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
    }
}

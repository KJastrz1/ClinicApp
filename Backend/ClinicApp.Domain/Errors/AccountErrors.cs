using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class AccountErrors
{
    public static Error NotFound(UserId id) => new(
        "Users.NotFound",
        $"User with ID {id.Value} was not found.");

    public static readonly Error EmptyId = new(
        "Users.EmptyId",
        "User ID is empty");

    public static class EmailErrors
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error TooLong = new(
            "Email.TooLong",
            $"Email is too long. Maximum length is 255 characters.");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");

        public static readonly Error EmailAlreadyInUse = new(
            "Email.EmailAlreadyInUse",
            "Email is already in use");
    }

    public static class PasswordErrors
    {
        public static readonly Error Empty = new(
            "Password.Empty",
            "Password is empty");

        public static readonly Error TooShort = new(
            "Password.TooShort",
            "Password must be at least 8 characters long.");

        public static readonly Error MissingUppercase = new(
            "Password.MissingUppercase",
            "Password must contain at least one uppercase letter.");

        public static readonly Error MissingLowercase = new(
            "Password.MissingLowercase",
            "Password must contain at least one lowercase letter.");

        public static readonly Error MissingDigit = new(
            "Password.MissingDigit",
            "Password must contain at least one digit.");

        public static readonly Error MissingSpecialCharacter = new(
            "Password.MissingSpecialCharacter",
            "Password must contain at least one special character.");

        public static readonly Error InvalidFormat = new(
            "Password.InvalidFormat",
            "Password format is invalid.");
    }

    public static class PasswordHashErrors
    {
        public static readonly Error Empty = new(
            "PasswordHash.Empty",
            "Password hash is empty");

        public static readonly Error InvalidFormat = new(
            "PasswordHash.InvalidFormat",
            "Password hash has an invalid format.");
    }

    public static class LoginErrors
    {
        public static readonly Error InvalidCredentials = new(
            "Login.InvalidCredentials",
            "Invalid username or password.");
        
        public static readonly Error AccountNotActivated = new(
            "Login.AccountNotActivated",
            "Account is not activated.");
    }
}

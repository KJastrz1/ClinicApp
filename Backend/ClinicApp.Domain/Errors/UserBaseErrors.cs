using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class UserBaseErrors
{
    public static Error EmptyId = new(
        "User.EmptyId",
        "User ID is empty");
    
    public static Error NotFound(Guid id) => new(
        "User.NotFound",
        $"User with ID {id} was not found.");

    public static class EmailErrors
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error TooLong = new(
            "Email.TooLong",
            $"Email is too long. Maximum length is {Email.MaxLength} characters.");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");

        public static readonly Error EmailAlreadyInUse = new(
            "Email.EmailAlreadyInUse",
            "Email is already in use");
    }

    public static class FirstNameErrors
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name is empty");

        public static readonly Error TooLong = new(
            "FirstName.TooLong",
            $"First name is too long. Maximum length is {FirstName.MaxLength} characters.");
    }

    public static class LastNameErrors
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            $"Last name is too long. Maximum length is {LastName.MaxLength} characters.");
    }
}

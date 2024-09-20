using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class UserBaseErrors
{
    public static Error NotFound(Guid id) => new(
        "Users.NotFound",
        $"Users with ID {id} was not found.");

    public static Error EmptyId = new(
        "Users.EmptyId",
        "Users ID is empty");

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

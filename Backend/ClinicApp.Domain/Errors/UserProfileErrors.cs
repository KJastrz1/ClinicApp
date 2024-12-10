using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class UserProfileErrors
{
    public static Error NotFound(UserId id) => new(
        "UserProfiles.NotFound",
        $"UserProfiles with ID {id.Value} was not found.");

    public static Error EmptyId = new(
        "UserProfiles.EmptyId",
        "UserProfiles ID is empty");

    public static Error AlreadyExists(UserId userId) => new(
        "UserProfiles.AlreadyExists",
        $"UserProfiles with ID {userId.Value} already exists.");

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

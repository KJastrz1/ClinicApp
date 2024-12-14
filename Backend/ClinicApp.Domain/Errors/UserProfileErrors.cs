using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class UserProfileErrors
{
    public static Error NotFound(UserId id) => new(
        "UserProfiles.NotFound",
        $"User Profile with ID {id.Value} was not found.");

    public static Error EmptyId = new(
        "UserProfiles.EmptyId",
        "User Profile ID is empty");

    public static Error AlreadyExists(UserId userId) => new(
        "UserProfiles.AlreadyExists",
        $"User Profile with ID {userId.Value} already exists.");

    public static Error RoleAssignmentFailed(UserId userId) => new(
        "UserProfiles.RoleAssignmentFailed",
        $"Failed to assign role to User Profile with ID {userId.Value}.");

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

using ClinicApp.Domain.Enums;

namespace Shared.Contracts.UserProfile.Responses;

public record UserProfileResponse(
    Guid Id,
    string FirstName,
    string LastName,
    UserRoleEnum Role,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc
);

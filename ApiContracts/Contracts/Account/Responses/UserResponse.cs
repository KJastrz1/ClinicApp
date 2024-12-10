namespace Shared.Contracts.Account.Responses;

public record UserResponse(
    Guid Id,
    string UserName,
    string Email,
    bool IsActivated,
    bool EmailConfirmed,
    string PhoneNumber,
    bool PhoneNumberConfirmed,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc
);

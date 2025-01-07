namespace Shared.Contracts.Auth.Responses;

public record UserResponse(
    Guid Id,
    string Email,
    List<string> Role,
    bool IsEmailConfirmed
);

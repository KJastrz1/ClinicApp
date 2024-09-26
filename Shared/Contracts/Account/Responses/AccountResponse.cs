namespace Shared.Contracts.Account.Responses;
public record AccountResponse(
    Guid Id,
    string Email,
    bool IsActivated,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc,
    List<RoleResponse> Roles);

namespace Shared.Contracts.Account;

public record AccountFilter(
    string? Email = null,
    bool? IsActivated = null,
    DateTime? CreatedOnUtcStart = null,
    DateTime? CreatedOnUtcEnd = null,
    DateTime? ModifiedOnUtcStart = null,
    DateTime? ModifiedOnUtcEnd = null,
    List<string>? Roles = null
);

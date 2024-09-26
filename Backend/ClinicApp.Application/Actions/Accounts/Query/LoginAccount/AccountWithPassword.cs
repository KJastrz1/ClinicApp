using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.Actions.Accounts.Query.LoginAccount;

public record AccountWithPassword(
    Guid Id,
    string Email,
    string PasswordHash,
    bool IsActivated,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc,
    List<RoleResponse> Roles);

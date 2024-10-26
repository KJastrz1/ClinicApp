using ClinicApp.Domain.Models.Accounts;
using Shared.Contracts.Account.Responses;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.UseCases.Accounts.Query.LoginAccount;

internal static class AccountMapper
{
    public static AccountResponse MapToResponse(this Account account)
    {
        return new AccountResponse(
            account.Id.Value,
            account.Email.Value,
            account.IsActivated,
            account.CreatedOnUtc,
            account.ModifiedOnUtc,
            account.Roles.Select(r => new RoleResponse(
                r.Id.Value,
                r.Name.Value,
                r.Permissions.Select(p => new PermissionResponse(p.Id.Value, p.Name)).ToList()
            )).ToList()
        );
    }
}

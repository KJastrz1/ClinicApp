using ClinicApp.Domain.Models.Accounts;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.Mappings;

public static class AccountMappings
{
    public static AccountResponse ToResponse(this Account account)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        return new AccountResponse(
            Id: account.Id.Value,
            Email: account.Email.Value,
            IsActivated: account.IsActivated,
            CreatedOnUtc: account.CreatedOnUtc,
            ModifiedOnUtc: account.ModifiedOnUtc,
            Roles: account.Roles.Select(role => new RoleResponse(
                Name: role.Name.Value,
                Permissions: role.Permissions.Select(p => new PermissionResponse(p.Name)).ToList()
            )).ToList()
        );
    }
}

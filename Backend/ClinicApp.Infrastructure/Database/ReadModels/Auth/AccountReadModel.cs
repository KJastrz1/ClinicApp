using ClinicApp.Application.Actions.Accounts.Query.LoginAccount;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels.Auth;

internal class AccountReadModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsActivated { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public string? PasswordHash { get; set; } 
    public List<RoleReadModel> Roles { get; set; }
    
    internal AccountResponse MapToResponse()
    {
        return new AccountResponse(
            Id: Id,
            Email: Email,
            IsActivated: IsActivated,
            CreatedOnUtc: CreatedOnUtc,
            ModifiedOnUtc: ModifiedOnUtc,
            Roles: Roles.Select(role => new RoleResponse(
                Name: role.Name,
                Permissions: role.Permissions.Select(p => new PermissionResponse(p.Name)).ToList()
            )).ToList()
        );
    }
    
    internal AccountWithPassword MapToAccountWithPassword()
    {
        return new AccountWithPassword(
            Id: Id,
            Email: Email,
            PasswordHash: PasswordHash ?? string.Empty,
            IsActivated: IsActivated,
            CreatedOnUtc: CreatedOnUtc,
            ModifiedOnUtc: ModifiedOnUtc,
            Roles: Roles.Select(role => new RoleResponse(
                Name: role.Name,
                Permissions: role.Permissions.Select(p => new PermissionResponse(p.Name)).ToList()
            )).ToList()
        );
    }
}

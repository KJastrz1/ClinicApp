using Shared.Contracts.Account.Responses;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels.Auth;

internal class AccountReadModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsActivated { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
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
                Id: role.Id,
                Name: role.Name,
                Permissions: role.Permissions.Select(p => new PermissionResponse(p.Id, p.Name)).ToList()
            )).ToList()
        );
    }
}

using ClinicApp.Domain.Models.Permissions.ValueObjects;

namespace ClinicApp.Domain.Models.Permissions;

public class Permission
{
    public PermissionId Id { get; init; }

    public string Name { get; init; } = string.Empty;
}

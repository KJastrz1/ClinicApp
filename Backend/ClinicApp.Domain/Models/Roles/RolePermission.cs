using ClinicApp.Domain.Models.Roles.ValueObjects;

namespace ClinicApp.Domain.Models.Roles;

public class RolePermission
{
    public RoleId RoleId { get; set; }

    public int PermissionId { get; set; }
}

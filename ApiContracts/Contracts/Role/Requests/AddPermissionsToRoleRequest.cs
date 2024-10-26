namespace Shared.Contracts.Role.Requests;

public sealed record AddPermissionsToRoleRequest(
    List<int> PermissionsIds
);

namespace Shared.Contracts.Role.Requests;

public sealed record AddPermissionsToRoleRequest(
    Guid Id,
    List<int> PermissionsIds
);

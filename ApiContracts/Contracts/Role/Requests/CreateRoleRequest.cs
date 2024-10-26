namespace Shared.Contracts.Role.Requests;

public sealed record CreateRoleRequest(
    string Name,
    List<int> PermissionsIds
);

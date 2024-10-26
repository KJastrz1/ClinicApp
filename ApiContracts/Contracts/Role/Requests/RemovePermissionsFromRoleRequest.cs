namespace Shared.Contracts.Role.Requests;

public sealed record RemovePermissionsFromRoleRequest(
    List<int> PermissionsIds
);

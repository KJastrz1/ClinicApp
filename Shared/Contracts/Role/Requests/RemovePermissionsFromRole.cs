namespace Shared.Contracts.Role.Requests;

public sealed record RemovePermissionsFromRole(
    Guid Id,
    List<int> PermissionsIds
);

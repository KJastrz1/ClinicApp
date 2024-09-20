namespace Shared.Contracts.Auth;
public record RoleResponse(
    string Name,
    List<PermissionResponse> Permissions
);

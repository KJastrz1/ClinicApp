namespace Shared.Contracts.Role.Responses;
public record RoleResponse(
    Guid Id,
    string Name,
    List<PermissionResponse> Permissions
);

namespace Shared.Contracts.Account.Responses;
public record RoleResponse(
    string Name,
    List<PermissionResponse> Permissions
);

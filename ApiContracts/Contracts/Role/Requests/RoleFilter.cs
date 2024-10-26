namespace Shared.Contracts.Role.Requests;

public record RoleFilter
{
    public string? Name { get; init; }
    
    public string? PermissionName { get; init; }
}

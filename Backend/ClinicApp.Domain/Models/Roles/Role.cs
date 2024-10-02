using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Roles.DomainEvents;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Roles;

public sealed class Role : AggregateRoot<RoleId>
{
    private List<Permission> _permissions;

    private Role()
    {
        _permissions = new List<Permission>();
    }

    private Role(RoleId id, RoleName name, List<Permission> permissions) : base(id)
    {
        Name = name;
        _permissions = permissions ?? new List<Permission>();
    }

    public RoleName Name { get; private set; }

    public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

    public static Role Create(RoleId id, RoleName name, List<Permission> permissions = null)
    {
        var role = new Role(id, name, permissions ?? new List<Permission>());

        role.RaiseDomainEvent(new RoleCreatedDomainEvent(role.Id.Value));

        foreach (Permission permission in role._permissions)
        {
            role.RaiseDomainEvent(new RolePermissionAddedDomainEvent(role.Id.Value, permission.Id));
        }

        return role;
    }

    public void AddPermission(Permission permission)
    {
        if (!_permissions.Contains(permission))
        {
            _permissions.Add(permission);
            RaiseDomainEvent(new RolePermissionAddedDomainEvent(Id.Value, permission.Id));
        }
    }

    public void RemovePermission(Permission permission)
    {
        if (_permissions.Remove(permission))
        {
            RaiseDomainEvent(new RolePermissionRemovedDomainEvent(Id.Value, permission.Id));
        }
    }

    public void RemovePermissionById(int permissionId)
    {
        Permission? permission = _permissions.FirstOrDefault(p => p.Id == permissionId);
        if (permission != null)
        {
            RemovePermission(permission); 
        }
    }

    public void RemovePermissionByName(string permissionName)
    {
        Permission? permission =
            _permissions.FirstOrDefault(p => p.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase));
        if (permission != null)
        {
            RemovePermission(permission);
        }
    }

    public void RemovePermissionsByIds(List<int> permissionIds)
    {
        foreach (int permissionId in permissionIds)
        {
            RemovePermissionById(permissionId); 
        }
    }

    public void RemovePermissionsByNames(List<string> permissionNames)
    {
        foreach (string permissionName in permissionNames)
        {
            RemovePermissionByName(permissionName); 
        }
    }

    public bool HasPermission(Permission permission) => _permissions.Contains(permission);
}

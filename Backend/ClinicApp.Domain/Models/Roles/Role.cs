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

    public bool HasPermission(Permission permission) => _permissions.Contains(permission);
}

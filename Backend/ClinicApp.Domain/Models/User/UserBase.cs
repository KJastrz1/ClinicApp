using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.User.DomainEvents;
using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.User;

public abstract class UserBase<TId> : AggregateRoot<TId>, IAuditableEntity
    where TId : UserId
{
    private readonly List<Role> _roles;

    protected UserBase(
        TId id,
        Email email,
        FirstName firstName,
        LastName lastName,
        UserType userType) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        IsActivated = false;
        _roles = new List<Role>();
        CreatedOnUtc = DateTime.UtcNow;
        UserType = userType;
    }

    protected UserBase()
    {
        _roles = new List<Role>();
    }

    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public bool IsActivated { get; private set; }
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    public UserType UserType { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
        {
            RaiseDomainEvent(new UserNameChangedDomainEvent(Guid.NewGuid(), Id.Value));
        }

        FirstName = firstName;
        LastName = lastName;
    }

    public void ChangeEmail(Email newEmail)
    {
        if (!Email.Equals(newEmail))
        {
            Email oldEmail = Email;
            Email = newEmail;
            RaiseDomainEvent(new UserEmailChangedDomainEvent(Guid.NewGuid(), Id.Value, oldEmail.Value, newEmail.Value));
        }
    }

    public void AddRole(Role role)
    {
        if (!_roles.Contains(role))
        {
            _roles.Add(role);
            RaiseDomainEvent(new UserRoleAddedDomainEvent(Guid.NewGuid(), Id.Value, role.Id));
        }
    }

    public void RemoveRole(Role role)
    {
        if (_roles.Remove(role))
        {
            RaiseDomainEvent(new UserRoleRemovedDomainEvent(Guid.NewGuid(), Id.Value, role.Id));
        }
    }

    public void ClearRoles()
    {
        if (_roles.Any())
        {
            _roles.Clear();
            RaiseDomainEvent(new UserRolesClearedDomainEvent(Guid.NewGuid(), Id.Value));
        }
    }

    public bool HasRole(Role role) => _roles.Contains(role);
}

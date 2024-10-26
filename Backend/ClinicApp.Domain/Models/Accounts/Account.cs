using ClinicApp.Domain.Models.Accounts.DomainEvents;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Accounts;

public class Account : AggregateRoot<AccountId>, IAuditableEntity
{
    private List<Role> _roles;
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public bool IsActivated { get; private set; }
    public User User { get; private set; }
    
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    private Account() { }

    private Account(
        AccountId id,
        Email email,
        PasswordHash passwordHash) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        IsActivated = false;
        CreatedOnUtc = DateTime.UtcNow;
        _roles = new List<Role>();
    }

    public static Account Create(AccountId id, Email email, PasswordHash passwordHash)
    {
        var account = new Account(id, email, passwordHash);
        account.RaiseDomainEvent(new AccountRegisteredDomainEvent(account.Id.Value));
        return account;
    }

    public void ChangeEmail(Email newEmail)
    {
        if (!Email.Equals(newEmail))
        {
            Email oldEmail = Email;
            Email = newEmail;
            IsActivated = false;
            RaiseDomainEvent(new AccountEmailChangedDomainEvent(Id.Value, oldEmail.Value, newEmail.Value));
        }
    }

    public void ChangePassword(PasswordHash newPasswordHash)
    {
        if (!PasswordHash.Equals(newPasswordHash))
        {
            PasswordHash = newPasswordHash;
            RaiseDomainEvent(new AccountPasswordChangedDomainEvent(Id.Value));
        }
    }

    public void ActivateAccount()
    {
        if (!IsActivated)
        {
            IsActivated = true;
            RaiseDomainEvent(new AccountActivatedDomainEvent(Id.Value));
        }
    }

    public void DeactivateAccount()
    {
        if (IsActivated)
        {
            IsActivated = false;
            RaiseDomainEvent(new AccountDeactivatedDomainEvent(Id.Value));
        }
    }

    public void AddRole(Role role)
    {
        if (!_roles.Contains(role))
        {
            _roles.Add(role);
            RaiseDomainEvent(new AccountRoleAddedDomainEvent(Id.Value, role.Id));
        }
    }

    public void RemoveRole(Role role)
    {
        if (_roles.Remove(role))
        {
            RaiseDomainEvent(new AccountRoleRemovedDomainEvent(Id.Value, role.Id));
        }
    }

    public void ClearRoles()
    {
        if (_roles.Any())
        {
            _roles.Clear();
            RaiseDomainEvent(new AccountRolesClearedDomainEvent(Id.Value));
        }
        
    }

    public bool HasRole(Role role) => _roles.Contains(role);
}

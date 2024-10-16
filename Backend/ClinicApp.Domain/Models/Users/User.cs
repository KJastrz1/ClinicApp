using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Users.DomainEvents;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Users;

public abstract class User : AggregateRoot<UserId>, IAuditableEntity
{
    protected User(
        UserId id,
        FirstName firstName,
        LastName lastName,
        UserType userType,
        Account? account) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        CreatedOnUtc = DateTime.UtcNow;
        UserType = userType;
        Account = account;
        AccountId = account?.Id;
    }

    protected User() { }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public UserType UserType { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public AccountId? AccountId { get; set; }
    public Account? Account { get; private set; }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
        {
            string newFullName = $"{firstName} {lastName}";
            RaiseDomainEvent(new UsersFullNameChangedDomainEvent(Id.Value, newFullName));
        }
        FirstName = firstName;
        LastName = lastName;
    }
}

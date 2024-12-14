using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.UserProfiles.DomainEvents;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.UserProfiles;

public abstract class UserProfile : AggregateRoot<UserId>, IAuditableEntity
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public UserRole UserRole { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    protected UserProfile() { }

    protected UserProfile(
        UserId id,
        FirstName firstName,
        LastName lastName,
        UserRole userRole
     ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        CreatedOnUtc = DateTime.UtcNow;
        UserRole = userRole;
    }


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

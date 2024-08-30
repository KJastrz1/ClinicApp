using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.User.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("User ID cannot be an empty GUID.", nameof(value));
        }
        Value = value;
    }
    public static UserId New() => new(Guid.NewGuid());
    
    public static implicit operator UserId(Guid value) => new(value);
    public static implicit operator Guid(UserId userId) => userId.Value;
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }


}

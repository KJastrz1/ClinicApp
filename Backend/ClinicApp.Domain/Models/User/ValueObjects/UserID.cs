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
    private UserId() { }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator Guid(UserId userId) => userId.Value;
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Users.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static Result<UserId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, UserBaseErrors.EmptyId)
            .Map(id => new UserId(id));
    }

    public static UserId New() => new(Guid.NewGuid());

    public static implicit operator Guid(UserId userId) => userId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

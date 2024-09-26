using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Roles.ValueObjects;

public sealed class RoleId : ValueObject
{
    public Guid Value { get; }

    private RoleId(Guid value)
    {
        Value = value;
    }

    private RoleId() { }

    public static Result<RoleId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, RoleErrors.EmptyId)
            .Map(id => new RoleId(id));
    }

    public static RoleId New() => new(Guid.NewGuid());

    public static implicit operator Guid(RoleId roleId) => roleId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

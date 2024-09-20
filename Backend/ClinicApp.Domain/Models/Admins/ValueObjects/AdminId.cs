using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Admins.ValueObjects;

public class AdminId : ValueObject
{
    public Guid Value { get; }

    private AdminId(Guid value)
    {
        Value = value;
    }

    public static Result<AdminId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, UserBaseErrors.EmptyId)
            .Map(id => new AdminId(id));
    }

    public static AdminId New() => new(Guid.NewGuid());

    public static implicit operator Guid(AdminId adminId) => adminId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

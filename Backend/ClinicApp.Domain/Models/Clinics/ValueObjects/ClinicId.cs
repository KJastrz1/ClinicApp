using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public class ClinicId : ValueObject
{
    public Guid Value { get; }

    private ClinicId(Guid value)
    {
        Value = value;
    }

    public static Result<ClinicId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, ClinicErrors.EmptyId)
            .Map(id => new ClinicId(id));
    }

    public static ClinicId New() => new(Guid.NewGuid());

    public static implicit operator ClinicId(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

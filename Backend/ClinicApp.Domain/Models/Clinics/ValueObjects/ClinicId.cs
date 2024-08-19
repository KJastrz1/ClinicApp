using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public class ClinicId : ValueObject
{
    public Guid Value { get; }
    private ClinicId(Guid value)
    {
        Value = value;
    }

    public static ClinicId New() => new(Guid.NewGuid());
    
    public static implicit operator ClinicId(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class Bio : ValueObject
{
    public const int MaxLength = 1000;

    private Bio(string value)
    {
        Value = value;
    }

    private Bio() { }

    public string Value { get; private set; }

    public static Result<Bio> Create(string bio) =>
        Result.Create(bio)
            .Ensure(
                b => b.Length <= MaxLength,
                DoctorErrors.BioErrors.TooLong)
            .Map(b => new Bio(b));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class Specialty : ValueObject
{
    public const int MaxLength = 100;

    private Specialty(string value)
    {
        Value = value;
    }

    private Specialty() { }

    public string Value { get; private set; }

    public static Result<Specialty> Create(string specialty) =>
        Result.Create(specialty)
            .Ensure(
                s => !string.IsNullOrWhiteSpace(s),
                DoctorErrors.SpecialtyErrors.Empty)
            .Ensure(
                s => s.Length <= MaxLength,
                DoctorErrors.SpecialtyErrors.TooLong)
            .Map(s => new Specialty(s));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}

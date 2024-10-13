using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public sealed class ZipCode : ValueObject
{
    public const int MaxLength = 10;

    private ZipCode(string value) => Value = value;

    private ZipCode() { }

    public string Value { get; private set; }

    public static Result<ZipCode> Create(string zipCode) =>
        Result.Create(zipCode)
            .Ensure(
                z => !string.IsNullOrWhiteSpace(z),
                ClinicErrors.ZipCodeErrors.Required)
            .Ensure(
                z => z.Length <= MaxLength,
                ClinicErrors.ZipCodeErrors.TooLong)
            .Map(z => new ZipCode(z));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

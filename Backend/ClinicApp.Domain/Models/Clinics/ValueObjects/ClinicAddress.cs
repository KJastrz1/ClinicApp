using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public sealed class ClinicAddress : ValueObject
{
    public const int MaxLength = 200;

    private ClinicAddress(string value) => Value = value;

    private ClinicAddress() { }

    public string Value { get; private set; }

    public static Result<ClinicAddress> Create(string address) =>
        Result.Create(address)
            .Ensure(
                a => !string.IsNullOrWhiteSpace(a),
                ClinicErrors.AddressErrors.Required)
            .Ensure(
                a => a.Length <= MaxLength,
                ClinicErrors.AddressErrors.TooLong)
            .Map(a => new ClinicAddress(a));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

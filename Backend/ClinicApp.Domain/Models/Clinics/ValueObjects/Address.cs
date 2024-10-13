using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public sealed class Address : ValueObject
{
    public const int MaxLength = 200;

    private Address(string value) => Value = value;

    private Address() { }

    public string Value { get; private set; }

    public static Result<Address> Create(string address) =>
        Result.Create(address)
            .Ensure(
                a => !string.IsNullOrWhiteSpace(a),
                ClinicErrors.AddressErrors.Required)
            .Ensure(
                a => a.Length <= MaxLength,
                ClinicErrors.AddressErrors.TooLong)
            .Map(a => new Address(a));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

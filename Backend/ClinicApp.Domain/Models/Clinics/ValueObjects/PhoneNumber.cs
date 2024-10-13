using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    private PhoneNumber(string value) => Value = value;

    private PhoneNumber() { }

    public string Value { get; private set; }

    public static Result<PhoneNumber> Create(string phoneNumber) =>
        Result.Create(phoneNumber)
            .Ensure(
                p => !string.IsNullOrWhiteSpace(p),
                ClinicErrors.PhoneNumberErrors.Required)
            .Ensure(
                p => p.All(char.IsDigit),
                ClinicErrors.PhoneNumberErrors.Invalid)
            .Map(p => new PhoneNumber(p));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

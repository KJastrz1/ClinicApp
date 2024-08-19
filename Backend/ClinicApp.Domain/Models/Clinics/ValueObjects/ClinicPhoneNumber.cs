using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public sealed class ClinicPhoneNumber : ValueObject
{
    private ClinicPhoneNumber(string value) => Value = value;

    private ClinicPhoneNumber() { }

    public string Value { get; private set; }

    public static Result<ClinicPhoneNumber> Create(string phoneNumber) =>
        Result.Create(phoneNumber)
            .Ensure(
                p => !string.IsNullOrWhiteSpace(p),
                ClinicErrors.PhoneNumberErrors.Required)
            .Ensure(
                p => p.All(char.IsDigit),
                ClinicErrors.PhoneNumberErrors.Invalid)
            .Map(p => new ClinicPhoneNumber(p));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

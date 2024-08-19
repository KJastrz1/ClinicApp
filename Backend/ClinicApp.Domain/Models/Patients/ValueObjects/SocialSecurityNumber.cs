using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Patients.ValueObjects;

public sealed class SocialSecurityNumber : ValueObject
{
    public const int Length = 11;  // Format: XXX-XX-XXXX

    private SocialSecurityNumber(string value) => Value = value;

    private SocialSecurityNumber()
    {
    }

    public string Value { get; private set; }

    public static Result<SocialSecurityNumber> Create(string ssn) =>
        Result.Create(ssn)
            .Ensure(
                s => !string.IsNullOrWhiteSpace(s),
                PatientErrors.SocialSecurityNumberErrors.Empty)
            .Ensure(
                s => s.Length == Length,
                PatientErrors.SocialSecurityNumberErrors.InvalidLength)
            .Ensure(
                s => s.Count(c => c == '-') == 2,
                PatientErrors.SocialSecurityNumberErrors.InvalidFormat)
            .Ensure(
                s => s.Replace("-", "").All(char.IsDigit),
                PatientErrors.SocialSecurityNumberErrors.InvalidFormat)
            .Map(s => new SocialSecurityNumber(s));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

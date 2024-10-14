using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class MedicalLicenseNumber : ValueObject
{
    public const int MaxLength = 20;

    private MedicalLicenseNumber(string value)
    {
        Value = value;
    }

    private MedicalLicenseNumber() { }

    public string Value { get; private set; }

    public static Result<MedicalLicenseNumber> Create(string licenseNumber) =>
        Result.Create(licenseNumber)
            .Ensure(
                ln => !string.IsNullOrWhiteSpace(ln),
                DoctorErrors.MedicalLicenseNumberErrors.Empty)
            .Ensure(
                ln => ln.Length <= MaxLength,
                DoctorErrors.MedicalLicenseNumberErrors.TooLong)
            .Map(ln => new MedicalLicenseNumber(ln));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

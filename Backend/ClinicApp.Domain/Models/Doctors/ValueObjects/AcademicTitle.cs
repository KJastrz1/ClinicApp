using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class AcademicTitle : ValueObject
{
    public const int MaxLength = 100;

    private AcademicTitle(string value)
    {
        Value = value;
    }

    private AcademicTitle() { }

    public string Value { get; private set; }

    public static Result<AcademicTitle> Create(string title) =>
        Result.Create(title)
            .Ensure(
                t => !string.IsNullOrWhiteSpace(t),
                DoctorErrors.AcademicTitleErrors.Empty)
            .Ensure(
                t => t.Length <= MaxLength,
                DoctorErrors.AcademicTitleErrors.TooLong)
            .Map(t => new AcademicTitle(t));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
